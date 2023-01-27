using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Moderation.MuteStream;

internal class MuteStreamUseCase
{
    private readonly VonageHttpClient vonageHttpClient;

    internal MuteStreamUseCase(VonageHttpClient client) => this.vonageHttpClient = client;

    internal Task<Result<MuteStreamResponse>> MuteStreamAsync(Result<MuteStreamRequest> request) =>
        this.vonageHttpClient.SendWithResponseAsync<MuteStreamResponse, MuteStreamRequest>(request);
}