using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Moderation.DisconnectConnection;

/// <inheritdoc />
public class DisconnectConnectionUseCase : IDisconnectConnectionUseCase
{
    private readonly VideoHttpClient client;
    private readonly Func<string> generateToken;

    /// <summary>
    ///     Creates a new instance of use case.
    /// </summary>
    /// <param name="client">Custom Http Client to used for further connections.</param>
    /// <param name="generateToken">Function used for generating a token.</param>
    public DisconnectConnectionUseCase(VideoHttpClient client, Func<string> generateToken)
    {
        this.client = client;
        this.generateToken = generateToken;
    }

    /// <inheritdoc />
    public Task<Result<Unit>> DisconnectConnectionAsync(DisconnectConnectionRequest request) =>
        this.client.SendAsync(request, this.generateToken());
}