using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Vonage.Request;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Voice;

namespace Vonage.Video.Beta.Video.Sessions.GetStream;

/// <inheritdoc />
public class GetStreamUseCase : IGetStreamUseCase
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
    public GetStreamUseCase(Credentials credentials, HttpClient httpClient, ITokenGenerator tokenGenerator)
    {
        this.credentials = credentials;
        this.client = httpClient;
        this.jsonSerializer = new JsonSerializer();
        this.tokenGenerator = tokenGenerator;
    }

    public async Task<Result<GetStreamResponse>> GetStreamAsync(GetStreamRequest request)
    {
        var httpRequest = this.BuildRequestMessage(request);
        var response = await this.client.SendAsync(httpRequest);
        var responseContent = await response.Content.ReadAsStringAsync();
        return !response.IsSuccessStatusCode
            ? this.jsonSerializer
                .DeserializeObject<ErrorResponse>(responseContent)
                .Map(parsedError => HttpFailure.From(response.StatusCode, parsedError.Message))
                .Bind(failure => Result<GetStreamResponse>.FromFailure(failure))
            : this.jsonSerializer
                .DeserializeObject<GetStreamResponse>(responseContent)
                .Match(_ => _, Result<GetStreamResponse>.FromFailure);
    }

    private HttpRequestMessage BuildRequestMessage(GetStreamRequest request)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, request.GetEndpointPath());
        httpRequest.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", this.tokenGenerator.GenerateToken(this.credentials));
        return httpRequest;
    }
}