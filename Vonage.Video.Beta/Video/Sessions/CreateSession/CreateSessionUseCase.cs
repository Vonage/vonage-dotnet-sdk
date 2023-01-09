using System;
using System.Linq;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Sessions.CreateSession;

/// <inheritdoc />
internal class CreateSessionUseCase : ICreateSessionUseCase
{
    private readonly Func<string> generateToken;
    private readonly VideoHttpClient videoHttpClient;

    /// <summary>
    ///     Creates a new instance of use case.
    /// </summary>
    /// <param name="client">Custom Http Client to used for further connections.</param>
    /// <param name="generateToken">Function used for generating a token.</param>
    public CreateSessionUseCase(VideoHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.videoHttpClient = client;
    }

    /// <inheritdoc />
    public async Task<Result<CreateSessionResponse>> CreateSessionAsync(Result<CreateSessionRequest> request)
    {
        var result =
            await this.videoHttpClient.SendWithResponseAsync<CreateSessionResponse[], CreateSessionRequest>(request,
                this.generateToken());
        return result.Bind(GetFirstSessionIfAvailable);
    }

    private static Result<CreateSessionResponse> GetFirstSessionIfAvailable(CreateSessionResponse[] sessions) =>
        sessions.Any()
            ? sessions[0]
            : Result<CreateSessionResponse>.FromFailure(
                ResultFailure.FromErrorMessage(CreateSessionResponse.NoSessionCreated));
}