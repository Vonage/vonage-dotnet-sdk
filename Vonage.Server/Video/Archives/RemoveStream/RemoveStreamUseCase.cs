using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Archives.RemoveStream;

internal class RemoveStreamUseCase
{
    private readonly VonageHttpClient vonageHttpClient;

    internal RemoveStreamUseCase(VonageHttpClient client) => this.vonageHttpClient = client;

    internal Task<Result<Unit>> RemoveStreamAsync(Result<RemoveStreamRequest> request) =>
        this.vonageHttpClient.SendAsync(request);
}