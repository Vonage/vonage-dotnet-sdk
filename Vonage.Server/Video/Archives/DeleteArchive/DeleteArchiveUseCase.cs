using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Archives.DeleteArchive;

internal class DeleteArchiveUseCase
{
    private readonly VonageHttpClient vonageHttpClient;

    internal DeleteArchiveUseCase(VonageHttpClient client) => this.vonageHttpClient = client;

    internal Task<Result<Unit>> DeleteArchiveAsync(Result<DeleteArchiveRequest> request) =>
        this.vonageHttpClient.SendAsync(request);
}