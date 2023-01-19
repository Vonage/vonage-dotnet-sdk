using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Signaling.SendSignal;

internal class SendSignalUseCase
{
    private readonly Func<string> generateToken;
    private readonly VonageHttpClient VonageHttpClient;

    internal SendSignalUseCase(VonageHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.VonageHttpClient = client;
    }

    internal Task<Result<Unit>> SendSignalAsync(Result<SendSignalRequest> request) =>
        this.VonageHttpClient.SendAsync(request, this.generateToken());
}