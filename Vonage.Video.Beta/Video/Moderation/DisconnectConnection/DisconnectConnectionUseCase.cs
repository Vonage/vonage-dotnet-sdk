using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Moderation.DisconnectConnection;

internal class DisconnectConnectionUseCase
{
    private readonly VideoHttpClient videoHttpClient;
    private readonly Func<string> generateToken;

    internal DisconnectConnectionUseCase(VideoHttpClient videoHttpClient, Func<string> generateToken)
    {
        this.videoHttpClient = videoHttpClient;
        this.generateToken = generateToken;
    }

    internal Task<Result<Unit>> DisconnectConnectionAsync(Result<DisconnectConnectionRequest> request) =>
        this.videoHttpClient.SendAsync(request, this.generateToken());
}