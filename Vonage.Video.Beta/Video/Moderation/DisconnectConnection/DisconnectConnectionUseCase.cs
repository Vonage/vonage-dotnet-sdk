using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Moderation.DisconnectConnection;

/// <inheritdoc />
internal class DisconnectConnectionUseCase : IDisconnectConnectionUseCase
{
    private readonly VideoHttpClient videoHttpClient;
    private readonly Func<string> generateToken;

    /// <summary>
    ///     Creates a new instance of use case.
    /// </summary>
    /// <param name="videoHttpClient">Custom Http Client to used for further connections.</param>
    /// <param name="generateToken">Function used for generating a token.</param>
    public DisconnectConnectionUseCase(VideoHttpClient videoHttpClient, Func<string> generateToken)
    {
        this.videoHttpClient = videoHttpClient;
        this.generateToken = generateToken;
    }

    /// <inheritdoc />
    public Task<Result<Unit>> DisconnectConnectionAsync(Result<DisconnectConnectionRequest> request) =>
        this.videoHttpClient.SendAsync(request, this.generateToken());
}