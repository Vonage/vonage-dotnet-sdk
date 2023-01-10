using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Moderation.MuteStream;

internal class MuteStreamUseCase
{
    private readonly Func<string> generateToken;
    private readonly VideoHttpClient videoHttpClient;

    internal MuteStreamUseCase(VideoHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.videoHttpClient = client;
    }

    internal Task<Result<MuteStreamResponse>> MuteStreamAsync(Result<MuteStreamRequest> request) =>
        this.videoHttpClient.SendWithResponseAsync<MuteStreamResponse, MuteStreamRequest>(request,
            this.generateToken());
}