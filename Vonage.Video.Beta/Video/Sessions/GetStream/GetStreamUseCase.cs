using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common;

namespace Vonage.Video.Beta.Video.Sessions.GetStream;

/// <inheritdoc />
public class GetStreamUseCase : IGetStreamUseCase
{
    private readonly Func<string> generateToken;
    private readonly VideoHttpClient videoHttpClient;

    /// <summary>
    ///     Creates a new instance of use case.
    /// </summary>
    /// <param name="client">Custom Http Client to used for further connections.</param>
    /// <param name="generateToken">Function used for generating a token.</param>
    public GetStreamUseCase(VideoHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.videoHttpClient = client;
    }

    /// <inheritdoc />
    public Task<Result<GetStreamResponse>> GetStreamAsync(GetStreamRequest request) =>
        this.videoHttpClient.SendWithResponseAsync<GetStreamResponse>(request, this.generateToken());
}