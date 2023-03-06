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
    private readonly VonageHttpClient vonageClient;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="configuration">The client configuration.</param>
    public SignalingClient(VonageHttpClientConfiguration configuration) => this.vonageClient =
        new VonageHttpClient(configuration, JsonSerializerBuilder.Build());

    /// <summary>
    ///     Sends signals to a single participant in an active Vonage Video session.
    /// </summary>
    /// <param name="request">The signal request.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    public Task<Result<Unit>> SendSignalAsync(Result<SendSignalRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <summary>
    ///     Sends signals to all participants in an active Vonage Video session.
    /// </summary>
    /// <param name="request">The signal request.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    public Task<Result<Unit>> SendSignalsAsync(Result<SendSignalsRequest> request) =>
        this.vonageClient.SendAsync(request);
}