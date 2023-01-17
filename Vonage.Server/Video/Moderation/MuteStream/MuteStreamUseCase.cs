using System;
using System.Threading.Tasks;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Moderation.MuteStream;

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