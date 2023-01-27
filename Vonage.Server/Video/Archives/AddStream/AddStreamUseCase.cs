using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Archives.AddStream;

internal class AddStreamUseCase
{
    private readonly VonageHttpClient vonageHttpClient;

    internal AddStreamUseCase(VonageHttpClient client) => this.vonageHttpClient = client;

    internal Task<Result<Unit>> AddStreamAsync(Result<AddStreamRequest> request) =>
        this.vonageHttpClient.SendAsync(request);
}