using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Video.Moderation.DisconnectConnection;
using Vonage.Video.Moderation.MuteStream;
using Vonage.Video.Moderation.MuteStreams;

namespace Vonage.Video.Moderation;

/// <summary>
///     Represents a client exposing moderation features.
/// </summary>
public class ModerationClient
{
    private readonly VonageHttpClient vonageClient;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="configuration">The client configuration.</param>
    public ModerationClient(VonageHttpClientConfiguration configuration) => this.vonageClient =
        new VonageHttpClient(configuration, JsonSerializerBuilder.Build());

    /// <summary>
    ///     Forces a client to disconnect from a session
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    public Task<Result<Unit>> DisconnectConnectionAsync(Result<DisconnectConnectionRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <summary>
    ///     Mutes a specific publisher stream
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success with the stream information if the operation succeeds, Failure it if fails.</returns>
    public Task<Result<MuteStreamResponse>> MuteStreamAsync(Result<MuteStreamRequest> request) =>
        this.vonageClient.SendWithResponseAsync<MuteStreamRequest, MuteStreamResponse>(request);

    /// <summary>
    ///     Forces all streams (except for an optional list of streams) in a session to mute published audio. You can also use
    ///     this method to disable the force mute state of a session.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success with the stream information if the operation succeeds, Failure it if fails.</returns>
    public Task<Result<MuteStreamsResponse>> MuteStreamsAsync(Result<MuteStreamsRequest> request) =>
        this.vonageClient.SendWithResponseAsync<MuteStreamsRequest, MuteStreamsResponse>(request);
}