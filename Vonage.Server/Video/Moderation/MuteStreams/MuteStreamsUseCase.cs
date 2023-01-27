using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Moderation.MuteStreams;

internal class MuteStreamsUseCase
{
    private readonly VonageHttpClient vonageHttpClient;

    internal MuteStreamsUseCase(VonageHttpClient client) => this.vonageHttpClient = client;

    internal Task<Result<MuteStreamsResponse>> MuteStreamsAsync(Result<MuteStreamsRequest> request) =>
        this.vonageHttpClient.SendWithResponseAsync<MuteStreamsResponse, MuteStreamsRequest>(request);
}