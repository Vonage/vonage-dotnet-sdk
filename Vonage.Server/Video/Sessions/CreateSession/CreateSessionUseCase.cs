using System;
using System.Linq;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Sessions.CreateSession;

internal class CreateSessionUseCase
{
    private readonly Func<string> generateToken;
    private readonly VonageHttpClient VonageHttpClient;

    internal CreateSessionUseCase(VonageHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.VonageHttpClient = client;
    }

    private static Result<CreateSessionResponse> GetFirstSessionIfAvailable(CreateSessionResponse[] sessions) =>
        sessions.Any()
            ? sessions[0]
            : Result<CreateSessionResponse>.FromFailure(
                ResultFailure.FromErrorMessage(CreateSessionResponse.NoSessionCreated));

    internal async Task<Result<CreateSessionResponse>> CreateSessionAsync(Result<CreateSessionRequest> request)
    {
        var result =
            await this.VonageHttpClient.SendWithResponseAsync<CreateSessionResponse[], CreateSessionRequest>(request,
                this.generateToken());
        return result.Bind(GetFirstSessionIfAvailable);
    }
}