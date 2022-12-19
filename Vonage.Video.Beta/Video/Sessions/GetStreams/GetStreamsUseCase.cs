using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Vonage.Request;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Video.Sessions.GetStream;
using Vonage.Voice;

namespace Vonage.Video.Beta.Video.Sessions.GetStreams;

public class GetStreamsUseCase : IGetStreamsUseCase
{
    private readonly HttpClient client;
    private readonly Credentials credentials;
    private readonly JsonSerializer jsonSerializer;
    private readonly ITokenGenerator tokenGenerator;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="credentials">Credentials to be used for further connections.</param>
    /// <param name="httpClient">Http Client to used for further connections.</param>
    /// <param name="tokenGenerator">Generator for authentication tokens.</param>
    public GetStreamsUseCase(Credentials credentials, HttpClient httpClient, ITokenGenerator tokenGenerator)
    {
        this.credentials = credentials;
        this.client = httpClient;
        this.jsonSerializer = new JsonSerializer();
        this.tokenGenerator = tokenGenerator;
    }

    /// <inheritdoc />
    public async Task<Result<GetStreamsResponse>> GetStreamsAsync(GetStreamsRequest request)
    {
        var httpRequest = this.BuildRequestMessage(request);
        var response = await this.client.SendAsync(httpRequest);
        var responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            return string.IsNullOrWhiteSpace(responseContent)
                ? Result<GetStreamsResponse>.FromFailure(HttpFailure.From(response.StatusCode))
                : this.jsonSerializer
                    .DeserializeObject<ErrorResponse>(responseContent)
                    .Map(parsedError => HttpFailure.From(response.StatusCode, parsedError.Message))
                    .Bind(failure => Result<GetStreamsResponse>.FromFailure(failure));
        }

        return this.jsonSerializer
            .DeserializeObject<GetStreamsResponse>(responseContent)
            .Match(_ => _, Result<GetStreamsResponse>.FromFailure);
    }

    private HttpRequestMessage BuildRequestMessage(GetStreamsRequest request)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, request.GetEndpointPath());
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.GenerateToken());
        return httpRequest;
    }

    private string GenerateToken() =>
        this.tokenGenerator.GenerateToken(this.credentials.ApplicationId, this.credentials.ApplicationKey);
}