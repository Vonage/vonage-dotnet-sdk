using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Moderation.MuteStreams;

internal class MuteStreamsUseCase
{
    private readonly Func<string> generateToken;
    private readonly VideoHttpClient videoHttpClient;

    internal MuteStreamsUseCase(VideoHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.videoHttpClient = client;
    }

    internal Task<Result<MuteStreamsResponse>> MuteStreamsAsync(Result<MuteStreamsRequest> request) =>
        this.videoHttpClient.SendWithResponseAsync<MuteStreamsResponse, MuteStreamsRequest>(request,
            this.generateToken());
}