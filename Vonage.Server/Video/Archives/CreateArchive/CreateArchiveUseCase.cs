using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Server.Video.Archives.Common;

namespace Vonage.Server.Video.Archives.CreateArchive;

internal class CreateArchiveUseCase
{
    private readonly VonageHttpClient vonageHttpClient;

    internal CreateArchiveUseCase(VonageHttpClient client) => this.vonageHttpClient = client;

    internal Task<Result<Archive>> CreateArchiveAsync(Result<CreateArchiveRequest> request) =>
        this.vonageHttpClient.SendWithResponseAsync<Archive, CreateArchiveRequest>(request);
}