using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Signaling.SendSignals;

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