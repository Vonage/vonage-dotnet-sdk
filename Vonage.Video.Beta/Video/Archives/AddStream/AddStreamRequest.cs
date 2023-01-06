using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Common.Validation;

namespace Vonage.Video.Beta.Video.Archives.AddStream;

/// <summary>
///     Represents a request to add a stream to an archive.
/// </summary>
public readonly struct AddStreamRequest : IVideoRequest
{
    private AddStreamRequest(string applicationId, string archiveId, string streamId, bool hasAudio, bool hasVideo)
    {
        this.ApplicationId = applicationId;
        this.ArchiveId = archiveId;
        this.StreamId = streamId;
        this.HasAudio = hasAudio;
        this.HasVideo = hasVideo;
    }

    /// <summary>
    ///     Whether the composed archive should include the stream's audio (true, the default) or not (false).
    /// </summary>
    public bool HasAudio { get; }

    /// <summary>
    ///     Whether the composed archive should include the stream's video (true, the default) or not (false).
    /// </summary>
    public bool HasVideo { get; }

    /// <summary>
    ///     The application Id.
    /// </summary>
    public string ApplicationId { get; }

    /// <summary>
    ///     The archive Id.
    /// </summary>
    public string ArchiveId { get; }

    /// <summary>
    ///     The stream Id.
    /// </summary>
    public string StreamId { get; }

    /// <summary>
    ///     Parses the input into a RemoveStreamRequest.
    /// </summary>
    /// <param name="applicationId">The application Id.</param>
    /// <param name="archiveId">The archive Id.</param>
    /// <param name="streamId">The stream Id.</param>
    /// <param name="hasAudio">
    ///     Whether the composed archive should include the stream's audio (true, the default) or not
    ///     (false).
    /// </param>
    /// <param name="hasVideo">
    ///     Whether the composed archive should include the stream's video (true, the default) or not
    ///     (false).
    /// </param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<AddStreamRequest> Parse(string applicationId, string archiveId, string streamId,
        bool hasAudio = true, bool hasVideo = true) =>
        Result<AddStreamRequest>
            .FromSuccess(new AddStreamRequest(applicationId, archiveId, streamId, hasAudio, hasVideo))
            .Bind(VerifyApplicationId)
            .Bind(VerifyArchiveId)
            .Bind(VerifyStreamId);

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/archive/{this.ArchiveId}/streams";

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage(string token)
    {
        var httpRequest = new HttpRequestMessage(new HttpMethod("PATCH"), this.GetEndpointPath());
        httpRequest.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        httpRequest.Content = new StringContent(
            new JsonSerializer().SerializeObject(new {AddStream = this.StreamId, this.HasAudio, this.HasVideo}),
            Encoding.UTF8,
            "application/json");
        return httpRequest;
    }

    private static Result<AddStreamRequest> VerifyApplicationId(AddStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<AddStreamRequest> VerifyArchiveId(AddStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ArchiveId, nameof(ArchiveId));

    private static Result<AddStreamRequest> VerifyStreamId(AddStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.StreamId, nameof(StreamId));
}