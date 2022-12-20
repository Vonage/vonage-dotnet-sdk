using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;

namespace Vonage.Video.Beta.Video.Sessions.CreateSession;

/// <inheritdoc />
public class CreateSessionUseCase : ICreateSessionUseCase
{
    private readonly HttpClient client;
    private readonly Func<string> generateToken;
    private readonly JsonSerializer jsonSerializer;

    /// <summary>
    ///     Creates a new instance of use case.
    /// </summary>
    /// <param name="httpClient">Http Client to used for further connections.</param>
    /// <param name="generateToken">Function used for generating a token.</param>
    public CreateSessionUseCase(HttpClient httpClient, Func<string> generateToken)
    {
        this.client = httpClient;
        this.generateToken = generateToken;
        this.jsonSerializer = new JsonSerializer();
    }

    /// <inheritdoc />
    public async Task<Result<CreateSessionResponse>> CreateSessionAsync(CreateSessionRequest request)
    {
        var httpRequest = request.BuildRequestMessage(this.generateToken());
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