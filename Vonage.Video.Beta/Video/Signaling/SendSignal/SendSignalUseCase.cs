using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Signaling.SendSignal;

internal class SendSignalUseCase
{
    private readonly Func<string> generateToken;
    private readonly VideoHttpClient videoHttpClient;

    internal SendSignalUseCase(VideoHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.videoHttpClient = client;
    }

    internal Task<Result<Unit>> SendSignalAsync(Result<SendSignalRequest> request) =>
        this.videoHttpClient.SendAsync(request, this.generateToken());
}