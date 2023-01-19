using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Signaling.SendSignals;

internal class SendSignalsUseCase
{
    private readonly Func<string> generateToken;
    private readonly VonageHttpClient VonageHttpClient;

    internal SendSignalsUseCase(VonageHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.VonageHttpClient = client;
    }

    internal Task<Result<Unit>> SendSignalsAsync(Result<SendSignalsRequest> request) =>
        this.VonageHttpClient.SendAsync(request, this.generateToken());
}