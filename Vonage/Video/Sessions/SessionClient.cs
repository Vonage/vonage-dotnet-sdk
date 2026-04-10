#region
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Video.Sessions.ChangeStreamLayout;
using Vonage.Video.Sessions.CreateSession;
using Vonage.Video.Sessions.GetStream;
using Vonage.Video.Sessions.GetStreams;
using Vonage.Video.Sessions.ListConnections;
#endregion

namespace Vonage.Video.Sessions;

/// <summary>
///     Represents a client exposing session features.
/// </summary>
public class SessionClient
{
    private readonly CreateSessionUseCase createSessionUseCase;
    private readonly VonageHttpClient<VideoApiError> vonageClient;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="configuration">The client configuration.</param>
    internal SessionClient(VonageHttpClientConfiguration configuration)
    {
        this.vonageClient =
            new VonageHttpClient<VideoApiError>(configuration, JsonSerializerBuilder.BuildWithCamelCase());
        this.createSessionUseCase = new CreateSessionUseCase(this.vonageClient);
    }

    /// <summary>
    ///     Changes how the stream is displayed in the layout of a composed Vonage Video archive.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = ChangeStreamLayoutRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .WithSessionId(sessionId)
    ///     .WithItems(new[] { new ChangeStreamLayoutRequest.LayoutItem("streamId", new[] { "full" }) })
    ///     .Create();
    /// var result = await client.VideoClient.SessionClient.ChangeStreamLayoutAsync(request);
    /// ]]></code>
    /// </example>
    public Task<Result<Unit>> ChangeStreamLayoutAsync(Result<ChangeStreamLayoutRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <summary>
    ///     Creates a new session.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = CreateSessionRequest.Build()
    ///     .WithLocation("192.168.1.1")
    ///     .WithMediaMode(MediaMode.Routed)
    ///     .WithArchiveMode(ArchiveMode.Manual)
    ///     .Create();
    /// var result = await client.VideoClient.SessionClient.CreateSessionAsync(request);
    /// ]]></code>
    /// </example>
    public Task<Result<CreateSessionResponse>> CreateSessionAsync(Result<CreateSessionRequest> request) =>
        this.createSessionUseCase.CreateSessionAsync(request);

    /// <summary>
    ///     Retrieves a stream.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the stream if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = GetStreamRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .WithSessionId(sessionId)
    ///     .WithStreamId(streamId)
    ///     .Create();
    /// var result = await client.VideoClient.SessionClient.GetStreamAsync(request);
    /// ]]></code>
    /// </example>
    public Task<Result<GetStreamResponse>> GetStreamAsync(Result<GetStreamRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetStreamRequest, GetStreamResponse>(request);

    /// <summary>
    ///     Retrieves information on all Vonage Video streams in a session.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A success state with streams if the operation succeeded. A failure state with the error message if it failed.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = GetStreamsRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .WithSessionId(sessionId)
    ///     .Create();
    /// var result = await client.VideoClient.SessionClient.GetStreamsAsync(request);
    /// ]]></code>
    /// </example>
    public Task<Result<GetStreamsResponse>> GetStreamsAsync(Result<GetStreamsRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetStreamsRequest, GetStreamsResponse>(request);

    /// <summary>
    ///     List the connections from a Vonage Video session associated with an application.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A success state with streams if the operation succeeded. A failure state with the error message if it failed.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = ListConnectionsRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .WithSessionId(sessionId)
    ///     .Create();
    /// var result = await client.VideoClient.SessionClient.ListConnections(request);
    /// ]]></code>
    /// </example>
    public Task<Result<ListConnectionsResponse>> ListConnections(Result<ListConnectionsRequest> request) =>
        this.vonageClient.SendWithResponseAsync<ListConnectionsRequest, ListConnectionsResponse>(request);
}