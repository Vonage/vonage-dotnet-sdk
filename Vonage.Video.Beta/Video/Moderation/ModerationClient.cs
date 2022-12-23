using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Video.Moderation.DisconnectConnection;

namespace Vonage.Video.Beta.Video.Moderation;

/// <inheritdoc />
public class ModerationClient : IModerationClient
{
    private readonly DisconnectConnectionUseCase disconnectConnectionUseCase;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="httpClient">Http Client to used for further connections.</param>
    /// <param name="tokenGeneration">Function used for generating a token.</param>
    public ModerationClient(HttpClient httpClient, Func<string> tokenGeneration)
    {
        this.disconnectConnectionUseCase =
            new DisconnectConnectionUseCase(new VideoHttpClient(httpClient), tokenGeneration);
    }

    /// <inheritdoc />
    public Task<Result<Unit>> DisconnectConnectionAsync(DisconnectConnectionRequest request) =>
        this.disconnectConnectionUseCase.DisconnectConnectionAsync(request);
}