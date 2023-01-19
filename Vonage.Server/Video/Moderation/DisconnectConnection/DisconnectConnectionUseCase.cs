using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Moderation.DisconnectConnection;

internal class DisconnectConnectionUseCase
{
    private readonly Func<string> generateToken;
    private readonly VonageHttpClient VonageHttpClient;

    internal DisconnectConnectionUseCase(VonageHttpClient VonageHttpClient, Func<string> generateToken)
    {
        this.VonageHttpClient = VonageHttpClient;
        this.generateToken = generateToken;
    }

    internal Task<Result<Unit>> DisconnectConnectionAsync(Result<DisconnectConnectionRequest> request) =>
        this.VonageHttpClient.SendAsync(request, this.generateToken());
}