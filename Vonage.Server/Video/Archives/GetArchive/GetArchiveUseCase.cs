using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Server.Video.Archives.Common;

namespace Vonage.Server.Video.Archives.GetArchive;

internal class GetArchiveUseCase
{
    private readonly VonageHttpClient vonageHttpClient;

    internal GetArchiveUseCase(VonageHttpClient client) => this.vonageHttpClient = client;

    internal Task<Result<Archive>> GetArchiveAsync(Result<GetArchiveRequest> request) =>
        this.vonageHttpClient.SendWithResponseAsync<Archive, GetArchiveRequest>(request);
}