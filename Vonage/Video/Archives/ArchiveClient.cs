#region
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Video.Archives.AddStream;
using Vonage.Video.Archives.ChangeLayout;
using Vonage.Video.Archives.CreateArchive;
using Vonage.Video.Archives.DeleteArchive;
using Vonage.Video.Archives.GetArchive;
using Vonage.Video.Archives.GetArchives;
using Vonage.Video.Archives.RemoveStream;
using Vonage.Video.Archives.StopArchive;
#endregion

namespace Vonage.Video.Archives;

/// <summary>
///     Represents a client exposing archiving features.
/// </summary>
public class ArchiveClient
{
    private readonly VonageHttpClient<VideoApiError> vonageClient;

    internal ArchiveClient(VonageHttpClientConfiguration configuration) => this.vonageClient =
        new VonageHttpClient<VideoApiError>(configuration, JsonSerializerBuilder.BuildWithCamelCase());

    /// <summary>
    ///     Adds the stream included in a composed archive that was started with the streamMode set to "manual".
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = AddStreamRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .WithArchiveId(archiveId)
    ///     .WithStreamId(streamId)
    ///     .Create();
    /// var result = await client.VideoClient.ArchiveClient.AddStreamAsync(request);
    /// ]]></code>
    /// </example>
    public Task<Result<Unit>> AddStreamAsync(Result<AddStreamRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <summary>
    ///     Changes the layout type of a composed archive while it is being recorded.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = ChangeLayoutRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .WithArchiveId(archiveId)
    ///     .WithLayout(new Layout(null, null, LayoutType.BestFit))
    ///     .Create();
    /// var result = await client.VideoClient.ArchiveClient.ChangeLayoutAsync(request);
    /// ]]></code>
    /// </example>
    public Task<Result<Unit>> ChangeLayoutAsync(Result<ChangeLayoutRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <summary>
    ///     Creates a new archive.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the archive if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = CreateArchiveRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .WithSessionId(sessionId)
    ///     .WithName("My Archive")
    ///     .Create();
    /// var result = await client.VideoClient.ArchiveClient.CreateArchiveAsync(request);
    /// ]]></code>
    /// </example>
    public Task<Result<Archive>> CreateArchiveAsync(Result<CreateArchiveRequest> request) =>
        this.vonageClient.SendWithResponseAsync<CreateArchiveRequest, Archive>(request);

    /// <summary>
    ///     Deletes the specified archive.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = DeleteArchiveRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .WithArchiveId(archiveId)
    ///     .Create();
    /// var result = await client.VideoClient.ArchiveClient.DeleteArchiveAsync(request);
    /// ]]></code>
    /// </example>
    public Task<Result<Unit>> DeleteArchiveAsync(Result<DeleteArchiveRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <summary>
    ///     Return the archive information of a specific archive.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the archive if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = GetArchiveRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .WithArchiveId(archiveId)
    ///     .Create();
    /// var result = await client.VideoClient.ArchiveClient.GetArchiveAsync(request);
    /// ]]></code>
    /// </example>
    public Task<Result<Archive>> GetArchiveAsync(Result<GetArchiveRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetArchiveRequest, Archive>(request);

    /// <summary>
    ///     Retrieves all archives from an application.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A success state with archives if the operation succeeded. A failure state with the error message if it failed.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = GetArchivesRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .Create();
    /// var result = await client.VideoClient.ArchiveClient.GetArchivesAsync(request);
    /// ]]></code>
    /// </example>
    public Task<Result<GetArchivesResponse>> GetArchivesAsync(Result<GetArchivesRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetArchivesRequest, GetArchivesResponse>(request);

    /// <summary>
    ///     Removes the stream included in a composed archive that was started with the streamMode set to "manual".
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = RemoveStreamRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .WithArchiveId(archiveId)
    ///     .WithStreamId(streamId)
    ///     .Create();
    /// var result = await client.VideoClient.ArchiveClient.RemoveStreamAsync(request);
    /// ]]></code>
    /// </example>
    public Task<Result<Unit>> RemoveStreamAsync(Result<RemoveStreamRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <summary>
    ///     Stops an archive.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the archive if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = StopArchiveRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .WithArchiveId(archiveId)
    ///     .Create();
    /// var result = await client.VideoClient.ArchiveClient.StopArchiveAsync(request);
    /// ]]></code>
    /// </example>
    public Task<Result<Archive>> StopArchiveAsync(Result<StopArchiveRequest> request) =>
        this.vonageClient.SendWithResponseAsync<StopArchiveRequest, Archive>(request);
}