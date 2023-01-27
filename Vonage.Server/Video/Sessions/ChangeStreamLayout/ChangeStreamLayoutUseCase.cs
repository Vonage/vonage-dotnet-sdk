using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Sessions.ChangeStreamLayout;

internal class ChangeStreamLayoutUseCase
{
    private readonly VonageHttpClient vonageHttpClient;

    internal ChangeStreamLayoutUseCase(VonageHttpClient client) => this.vonageHttpClient = client;

    internal Task<Result<Unit>> ChangeStreamLayoutAsync(Result<ChangeStreamLayoutRequest> request) =>
        this.vonageHttpClient.SendAsync(request);
}