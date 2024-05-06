using System.Linq;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;

namespace Vonage.Video.Sessions.CreateSession;

internal class CreateSessionUseCase
{
    private readonly VonageHttpClient vonageHttpClient;
    
    internal CreateSessionUseCase(VonageHttpClient client) => this.vonageHttpClient = client;
    
    private static Result<CreateSessionResponse> GetFirstSessionIfAvailable(CreateSessionResponse[] sessions) =>
        sessions.Any()
            ? sessions[0]
            : Result<CreateSessionResponse>.FromFailure(
                ResultFailure.FromErrorMessage(CreateSessionResponse.NoSessionCreated));
    
    internal async Task<Result<CreateSessionResponse>> CreateSessionAsync(Result<CreateSessionRequest> request)
    {
        var result =
            await this.vonageHttpClient.SendWithResponseAsync<CreateSessionRequest, CreateSessionResponse[]>(request)
                .ConfigureAwait(false);
        return result.Bind(GetFirstSessionIfAvailable);
    }
}