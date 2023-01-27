using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Server.Video.Archives.Common;

namespace Vonage.Server.Video.Archives.StopArchive;

internal class StopArchiveUseCase
{
    private readonly VonageHttpClient vonageHttpClient;

    internal StopArchiveUseCase(VonageHttpClient client) => this.vonageHttpClient = client;

    internal Task<Result<Archive>> StopArchiveAsync(Result<StopArchiveRequest> request) =>
        this.vonageHttpClient.SendWithResponseAsync<Archive, StopArchiveRequest>(request);
}