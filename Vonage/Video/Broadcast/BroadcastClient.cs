#region
using System.Threading.Tasks;
using Vonage.Common;
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
#endregion

namespace Vonage.Video.Broadcast;

/// <summary>
///     Represents a client exposing live broadcast features.
/// </summary>
public class BroadcastClient
{
    private readonly VonageHttpClient<VideoApiError> vonageClient;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="configuration">The client configuration.</param>
    internal BroadcastClient(VonageHttpClientConfiguration configuration) => this.vonageClient =
        new VonageHttpClient<VideoApiError>(configuration, JsonSerializerBuilder.BuildWithCamelCase());

    /// <summary>
    ///     Adds a stream to a live streaming broadcast.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state if the operation succeeded. A failure state with the error message if it failed.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = AddStreamToBroadcastRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .WithBroadcastId(broadcastId)
    ///     .WithStreamId(streamId)
    ///     .Create();
    /// var result = await client.VideoClient.BroadcastClient.AddStreamToBroadcastAsync(request);
    /// ]]></code>
    /// </example>
    public Task<Result<Unit>> AddStreamToBroadcastAsync(Result<AddStreamToBroadcastRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <summary>
    ///     Dynamically changes the layout type of a live streaming broadcast.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state if the operation succeeded. A failure state with the error message if it failed.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = ChangeBroadcastLayoutRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .WithBroadcastId(broadcastId)
    ///     .WithLayout(new Layout(null, null, LayoutType.BestFit))
    ///     .Create();
    /// var result = await client.VideoClient.BroadcastClient.ChangeBroadcastLayoutAsync(request);
    /// ]]></code>
    /// </example>
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
    /// <example>
    /// <code><![CDATA[
    /// var request = GetBroadcastRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .WithBroadcastId(broadcastId)
    ///     .Create();
    /// var result = await client.VideoClient.BroadcastClient.GetBroadcastAsync(request);
    /// ]]></code>
    /// </example>
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
    /// <example>
    /// <code><![CDATA[
    /// var request = GetBroadcastsRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .Create();
    /// var result = await client.VideoClient.BroadcastClient.GetBroadcastsAsync(request);
    /// ]]></code>
    /// </example>
    public Task<Result<GetBroadcastsResponse>> GetBroadcastsAsync(Result<GetBroadcastsRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetBroadcastsRequest, GetBroadcastsResponse>(request);

    /// <summary>
    ///     Removes a stream from a live streaming broadcast.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state if the operation succeeded. A failure state with the error message if it failed.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = RemoveStreamFromBroadcastRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .WithBroadcastId(broadcastId)
    ///     .WithStreamId(streamId)
    ///     .Create();
    /// var result = await client.VideoClient.BroadcastClient.RemoveStreamFromBroadcastAsync(request);
    /// ]]></code>
    /// </example>
    public Task<Result<Unit>> RemoveStreamFromBroadcastAsync(Result<RemoveStreamFromBroadcastRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <summary>
    ///     Starts a live stream broadcast for a Vonage Video session.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the broadcast if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = StartBroadcastRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .WithSessionId(sessionId)
    ///     .WithLayout(new Layout(null, null, LayoutType.BestFit))
    ///     .WithOutputs(outputs)
    ///     .Create();
    /// var result = await client.VideoClient.BroadcastClient.StartBroadcastsAsync(request);
    /// ]]></code>
    /// </example>
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
    /// <example>
    /// <code><![CDATA[
    /// var request = StopBroadcastRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .WithBroadcastId(broadcastId)
    ///     .Create();
    /// var result = await client.VideoClient.BroadcastClient.StopBroadcastAsync(request);
    /// ]]></code>
    /// </example>
    public Task<Result<Broadcast>> StopBroadcastAsync(Result<StopBroadcastRequest> request) =>
        this.vonageClient.SendWithResponseAsync<StopBroadcastRequest, Broadcast>(request);
}