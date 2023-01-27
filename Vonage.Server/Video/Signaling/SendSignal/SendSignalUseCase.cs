using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Signaling.SendSignal;

internal class SendSignalUseCase
{
    private readonly VonageHttpClient vonageHttpClient;

    internal SendSignalUseCase(VonageHttpClient client) => this.vonageHttpClient = client;

    internal Task<Result<Unit>> SendSignalAsync(Result<SendSignalRequest> request) =>
        this.vonageHttpClient.SendAsync(request);
}