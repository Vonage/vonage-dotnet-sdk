using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Video.Broadcast.AddStreamToBroadcast;
using Vonage.Video.Broadcast.ChangeBroadcastLayout;
using Vonage.Video.Broadcast.GetBroadcast;
using Vonage.Video.Broadcast.GetBroadcasts;
using Vonage.Video.Broadcast.RemoveStreamFromBroadcast;
using Vonage.Video.Broadcast.StartBroadcast;
using Vonage.Video.Broadcast.StopBroadcast;

namespace Vonage.Video.Broadcast;

/// <summary>
///     Represents a client exposing live broadcast features.
/// </summary>
public class BroadcastClient
{
    private readonly VonageHttpClient vonageClient;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="configuration">The client configuration.</param>
    public BroadcastClient(VonageHttpClientConfiguration configuration) => this.vonageClient =
        new VonageHttpClient(configuration, JsonSerializerBuilder.Build());

    /// <summary>
    ///     Adds a stream to a live streaming broadcast.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state if the operation succeeded. A failure state with the error message if it failed.
    /// </returns>
    public Task<Result<Unit>> AddStreamToBroadcastAsync(Result<AddStreamToBroadcastRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <summary>
    ///     Dynamically changes the layout type of a live streaming broadcast.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state if the operation succeeded. A failure state with the error message if it failed.
    /// </returns>
    public Task<Result<Unit>> ChangeBroadcastLayoutAsync(Result<ChangeBroadcastLayoutRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <summary>
    ///     Retrieves a live streaming broadcast.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the broadcast if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    public Task<Result<Broadcast>> GetBroadcastAsync(Result<GetBroadcastRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetBroadcastRequest, Broadcast>(request);

    /// <summary>
    ///     Retrieves all live streaming broadcasts.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with broadcasts if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    public Task<Result<GetBroadcastsResponse>> GetBroadcastsAsync(Result<GetBroadcastsRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetBroadcastsRequest, GetBroadcastsResponse>(request);

    /// <summary>
    ///     Removes a live streaming broadcast.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Task<Result<Unit>> RemoveStreamFromBroadcastAsync(Result<RemoveStreamFromBroadcastRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <summary>
    ///     Starts a live stream broadcast for an OpenTok session.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the broadcast if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    public Task<Result<Broadcast>> StartBroadcastsAsync(Result<StartBroadcastRequest> request) =>
        this.vonageClient.SendWithResponseAsync<StartBroadcastRequest, Broadcast>(request);

    /// <summary>
    ///     Stops a live stream broadcast.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the broadcast if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    public Task<Result<Broadcast>> StopBroadcastAsync(Result<StopBroadcastRequest> request) =>
        this.vonageClient.SendWithResponseAsync<StopBroadcastRequest, Broadcast>(request);
}