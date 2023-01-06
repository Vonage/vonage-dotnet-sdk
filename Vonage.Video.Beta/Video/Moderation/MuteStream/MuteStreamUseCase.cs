using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Moderation.MuteStream;

/// <inheritdoc />
public class MuteStreamUseCase : IMuteStreamUseCase
{
    private readonly Func<string> generateToken;
    private readonly VideoHttpClient videoHttpClient;

    /// <summary>
    ///     Creates a new instance of use case.
    /// </summary>
    /// <param name="client">Custom Http Client to used for further connections.</param>
    /// <param name="generateToken">Function used for generating a token.</param>
    public MuteStreamUseCase(VideoHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.videoHttpClient = client;
    }

    /// <inheritdoc />
    public Task<Result<MuteStreamResponse>> MuteStreamAsync(Result<MuteStreamRequest> request) =>
        this.videoHttpClient.SendWithResponseAsync<MuteStreamResponse, MuteStreamRequest>(request,
            this.generateToken());
}