using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Request;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Voice;

namespace Vonage.Video.Beta.Video.Sessions.CreateSession;

/// <inheritdoc />
public class CreateSessionUseCase : ICreateSessionUseCase
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
    public CreateSessionUseCase(Credentials credentials, HttpClient httpClient, ITokenGenerator tokenGenerator)
    {
        this.credentials = credentials;
        this.client = httpClient;
        this.jsonSerializer = new JsonSerializer();
        this.tokenGenerator = tokenGenerator;
    }

    /// <inheritdoc />
    public async Task<Result<CreateSessionResponse>> CreateSessionAsync(CreateSessionRequest request)
    {
        var httpRequest = request.BuildRequestMessage(this.tokenGenerator.GenerateToken(this.credentials));
        var response = await this.client.SendAsync(httpRequest);
        var responseContent = await response.Content.ReadAsStringAsync();
        return !response.IsSuccessStatusCode
            ? GetFailureFromErrorStatusCode(response.StatusCode, responseContent)
            : this.jsonSerializer
                .DeserializeObject<CreateSessionResponse[]>(responseContent)
                .Bind(GetFirstSessionIfAvailable);
    }

    private static Result<CreateSessionResponse> GetFailureFromErrorStatusCode(HttpStatusCode statusCode,
        string responseContent) =>
        Result<CreateSessionResponse>.FromFailure(HttpFailure.From(statusCode, responseContent));

    private static Result<CreateSessionResponse> GetFirstSessionIfAvailable(CreateSessionResponse[] sessions) =>
        sessions.Any()
            ? sessions.First()
            : Result<CreateSessionResponse>.FromFailure(
                ResultFailure.FromErrorMessage(CreateSessionResponse.NoSessionCreated));
}