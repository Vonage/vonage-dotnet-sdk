using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Moderation.DisconnectConnection;

internal class DisconnectConnectionUseCase
{
    private readonly VonageHttpClient vonageHttpClient;

    internal DisconnectConnectionUseCase(VonageHttpClient vonageHttpClient) => this.vonageHttpClient = vonageHttpClient;

    internal Task<Result<Unit>> DisconnectConnectionAsync(Result<DisconnectConnectionRequest> request) =>
        this.vonageHttpClient.SendAsync(request);
}