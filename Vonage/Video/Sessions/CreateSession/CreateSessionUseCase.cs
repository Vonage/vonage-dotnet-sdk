#region
using System.Linq;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
#endregion

namespace Vonage.Video.Sessions.CreateSession;

internal class CreateSessionUseCase
{
    private readonly VonageHttpClient<VideoApiError> vonageHttpClient;

    internal CreateSessionUseCase(VonageHttpClient<VideoApiError> client) => this.vonageHttpClient = client;

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