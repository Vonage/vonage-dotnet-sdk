using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Video.Signaling.SendSignals;

namespace Vonage.Video.Beta.Video.Signaling;

/// <inheritdoc />
public class SignalingClient : ISignalingClient
{
    private readonly SendSignalsUseCase sendSignalsUseCase;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="httpClient">Http Client to used for further connections.</param>
    /// <param name="tokenGeneration">Function used for generating a token.</param>
    public SignalingClient(HttpClient httpClient, Func<string> tokenGeneration)
    {
        this.sendSignalsUseCase = new SendSignalsUseCase(httpClient, tokenGeneration);
    }

    /// <inheritdoc />
    public Task<Result<Unit>> SendSignalsAsync(SendSignalsRequest request) =>
        this.sendSignalsUseCase.SendSignalsAsync(request);
}