#region
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Video.Signaling.SendSignal;
using Vonage.Video.Signaling.SendSignals;
#endregion

namespace Vonage.Video.Signaling;

/// <summary>
///     Represents a client exposing signaling features.
/// </summary>
public class SignalingClient
{
    private readonly VonageHttpClient<VideoApiError> vonageClient;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="configuration">The client configuration.</param>
    internal SignalingClient(VonageHttpClientConfiguration configuration) => this.vonageClient =
        new VonageHttpClient<VideoApiError>(configuration, JsonSerializerBuilder.BuildWithCamelCase());

    /// <summary>
    ///     Sends signals to a single participant in an active Vonage Video session.
    /// </summary>
    /// <param name="request">The signal request.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = SendSignalRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .WithSessionId(sessionId)
    ///     .WithConnectionId(connectionId)
    ///     .WithContent(new SignalContent("chat", "Hello!"))
    ///     .Create();
    /// var result = await client.VideoClient.SignalingClient.SendSignalAsync(request);
    /// ]]></code>
    /// </example>
    public Task<Result<Unit>> SendSignalAsync(Result<SendSignalRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <summary>
    ///     Sends signals to all participants in an active Vonage Video session.
    /// </summary>
    /// <param name="request">The signal request.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = SendSignalsRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .WithSessionId(sessionId)
    ///     .WithContent(new SignalContent("chat", "Hello everyone!"))
    ///     .Create();
    /// var result = await client.VideoClient.SignalingClient.SendSignalsAsync(request);
    /// ]]></code>
    /// </example>
    public Task<Result<Unit>> SendSignalsAsync(Result<SendSignalsRequest> request) =>
        this.vonageClient.SendAsync(request);
}