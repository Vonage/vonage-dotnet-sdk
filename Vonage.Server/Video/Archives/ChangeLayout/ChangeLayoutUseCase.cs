using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Archives.ChangeLayout;

internal class ChangeLayoutUseCase
{
    private readonly VonageHttpClient vonageHttpClient;

    internal ChangeLayoutUseCase(VonageHttpClient client) => this.vonageHttpClient = client;

    internal Task<Result<Unit>> ChangeLayoutAsync(Result<ChangeLayoutRequest> request) =>
        this.vonageHttpClient.SendAsync(request);
}