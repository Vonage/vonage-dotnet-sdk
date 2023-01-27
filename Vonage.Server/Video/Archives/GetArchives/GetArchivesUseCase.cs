using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Archives.GetArchives;

internal class GetArchivesUseCase
{
    private readonly VonageHttpClient vonageHttpClient;

    internal GetArchivesUseCase(VonageHttpClient client) => this.vonageHttpClient = client;

    internal Task<Result<GetArchivesResponse>> GetArchivesAsync(Result<GetArchivesRequest> request) =>
        this.vonageHttpClient.SendWithResponseAsync<GetArchivesResponse, GetArchivesRequest>(request);
}