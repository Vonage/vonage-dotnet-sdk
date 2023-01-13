using System;
using System.Linq;
using System.Threading.Tasks;
using Vonage.Server.Common.Failures;
using Vonage.Server.Common.Monads;

namespace Vonage.Server.Video.Sessions.CreateSession;

internal class CreateSessionUseCase
{
    private readonly Func<string> generateToken;
    private readonly VideoHttpClient videoHttpClient;

    internal CreateSessionUseCase(VideoHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.videoHttpClient = client;
    }

    private static Result<CreateSessionResponse> GetFirstSessionIfAvailable(CreateSessionResponse[] sessions) =>
        sessions.Any()
            ? sessions[0]
            : Result<CreateSessionResponse>.FromFailure(
                ResultFailure.FromErrorMessage(CreateSessionResponse.NoSessionCreated));

    internal async Task<Result<CreateSessionResponse>> CreateSessionAsync(Result<CreateSessionRequest> request)
    {
        var result =
            await this.videoHttpClient.SendWithResponseAsync<CreateSessionResponse[], CreateSessionRequest>(request,
                this.generateToken());
        return result.Bind(GetFirstSessionIfAvailable);
    }
}