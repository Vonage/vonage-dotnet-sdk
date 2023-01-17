using System;
using System.Threading.Tasks;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Signaling.SendSignals;

internal class SendSignalsUseCase
{
    private readonly Func<string> generateToken;
    private readonly VideoHttpClient videoHttpClient;

    internal SendSignalsUseCase(VideoHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.videoHttpClient = client;
    }

    internal Task<Result<Unit>> SendSignalsAsync(Result<SendSignalsRequest> request) =>
        this.videoHttpClient.SendAsync(request, this.generateToken());
}