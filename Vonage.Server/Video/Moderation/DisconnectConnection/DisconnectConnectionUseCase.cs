using System;
using System.Threading.Tasks;
using Vonage.Server.Common.Monads;

namespace Vonage.Server.Video.Moderation.DisconnectConnection;

internal class DisconnectConnectionUseCase
{
    private readonly Func<string> generateToken;
    private readonly VideoHttpClient videoHttpClient;

    internal DisconnectConnectionUseCase(VideoHttpClient videoHttpClient, Func<string> generateToken)
    {
        this.videoHttpClient = videoHttpClient;
        this.generateToken = generateToken;
    }

    internal Task<Result<Unit>> DisconnectConnectionAsync(Result<DisconnectConnectionRequest> request) =>
        this.videoHttpClient.SendAsync(request, this.generateToken());
}