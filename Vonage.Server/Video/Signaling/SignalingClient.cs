using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Server.Serialization;
using Vonage.Server.Video.Signaling.SendSignal;
using Vonage.Server.Video.Signaling.SendSignals;

namespace Vonage.Server.Video.Signaling;

/// <summary>
///     Represents a client exposing signaling features.
/// </summary>
public class SignalingClient
{
    private readonly SendSignalsUseCase sendSignalsUseCase;
    private readonly SendSignalUseCase sendSignalUseCase;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="httpClient">Http Client to used for further connections.</param>
    /// <param name="tokenGeneration">Function used for generating a token.</param>
    public SignalingClient(HttpClient httpClient, Func<string> tokenGeneration)
    {
        var client = new VonageHttpClient(httpClient, JsonSerializerBuilder.Build(), tokenGeneration);
        this.sendSignalUseCase = new SendSignalUseCase(client);
        this.sendSignalsUseCase = new SendSignalsUseCase(client);
    }

    /// <summary>
    ///     Sends signals to a single participant in an active Vonage Video session.
    /// </summary>
    /// <param name="request">The signal request.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    public Task<Result<Unit>> SendSignalAsync(Result<SendSignalRequest> request) =>
        this.sendSignalUseCase.SendSignalAsync(request);

    /// <summary>
    ///     Sends signals to all participants in an active Vonage Video session.
    /// </summary>
    /// <param name="request">The signal request.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    public Task<Result<Unit>> SendSignalsAsync(Result<SendSignalsRequest> request) =>
        this.sendSignalsUseCase.SendSignalsAsync(request);
}