using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Vonage.Request;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Video.Sessions.CreateSession;
using Vonage.Video.Beta.Video.Sessions.GetStream;
using Vonage.Voice;

namespace Vonage.Video.Beta.Video.Sessions;

/// <inheritdoc />
public class SessionClient : ISessionClient
{
    private readonly HttpClient client;
    private readonly JsonSerializer jsonSerializer;
    private readonly ITokenGenerator tokenGenerator;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="credentials">Credentials to be used for further connections.</param>
    /// <param name="httpClient">Http Client to used for further connections.</param>
    /// <param name="tokenGenerator">Generator for authentication tokens.</param>
    public SessionClient(Credentials credentials, HttpClient httpClient, ITokenGenerator tokenGenerator)
    {
        this.Credentials = credentials;
        this.client = httpClient;
        this.jsonSerializer = new JsonSerializer();
        this.tokenGenerator = tokenGenerator;
    }

    /// <inheritdoc />
    public Credentials Credentials { get; set; }

    /// <inheritdoc />
    public async Task<Result<CreateSessionResponse>> CreateSessionAsync(CreateSessionRequest request)
    {
        var httpRequest = this.BuildRequestMessage(request);
        var response = await this.client.SendAsync(httpRequest);
        var responseContent = await response.Content.ReadAsStringAsync();
        return !response.IsSuccessStatusCode
            ? GetFailureFromErrorStatusCode(response.StatusCode, responseContent)
            : this.jsonSerializer
                .DeserializeObject<CreateSessionResponse[]>(responseContent)
                .Bind(GetFirstSessionIfAvailable);
    }

    /// <inheritdoc />
    public async Task<Result<GetStreamResponse>> GetStreamAsync(GetStreamRequest request)
    {
        var httpRequest = this.BuildRequestMessage(request);
        var response = await this.client.SendAsync(httpRequest);
        var responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = this.jsonSerializer.DeserializeObject<ErrorResponse>(responseContent);
            return errorResponse
                .Map(err => HttpFailure.From(response.StatusCode, err.Message))
                .Bind(e => Result<GetStreamResponse>.FromFailure(e));
        }

        throw new NotImplementedException();
    }

    private HttpRequestMessage BuildRequestMessage(GetStreamRequest request)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, request.GetEndpointPath());
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.GenerateToken());
        return httpRequest;
    }

    private static Result<CreateSessionResponse> GetFailureFromErrorStatusCode(HttpStatusCode statusCode,
        string responseContent) =>
        Result<CreateSessionResponse>.FromFailure(HttpFailure.From(statusCode, responseContent));

    private HttpRequestMessage BuildRequestMessage(CreateSessionRequest request)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, CreateSessionRequest.CreateSessionEndpoint);
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.GenerateToken());
        httpRequest.Content =
            new StringContent(request.GetUrlEncoded(), Encoding.UTF8, "application/x-www-form-urlencoded");
        return httpRequest;
    }

    private string GenerateToken() =>
        this.tokenGenerator.GenerateToken(this.Credentials.ApplicationId, this.Credentials.ApplicationKey);

    private static Result<CreateSessionResponse> GetFirstSessionIfAvailable(CreateSessionResponse[] sessions) =>
        sessions.Any()
            ? sessions.First()
            : Result<CreateSessionResponse>.FromFailure(
                ResultFailure.FromErrorMessage(CreateSessionResponse.NoSessionCreated));
}