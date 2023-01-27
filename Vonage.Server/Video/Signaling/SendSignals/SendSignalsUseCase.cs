using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Signaling.SendSignals;

internal class SendSignalsUseCase
{
    private readonly VonageHttpClient vonageHttpClient;

    internal SendSignalsUseCase(VonageHttpClient client) => this.vonageHttpClient = client;

    internal Task<Result<Unit>> SendSignalsAsync(Result<SendSignalsRequest> request) =>
        this.vonageHttpClient.SendAsync(request);
}