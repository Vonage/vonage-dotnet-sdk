using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Sessions.GetStreams;

internal class GetStreamsUseCase
{
    private readonly VonageHttpClient vonageHttpClient;

    internal GetStreamsUseCase(VonageHttpClient client) => this.vonageHttpClient = client;

    internal Task<Result<GetStreamsResponse>> GetStreamsAsync(Result<GetStreamsRequest> request) =>
        this.vonageHttpClient.SendWithResponseAsync<GetStreamsResponse, GetStreamsRequest>(request);
}