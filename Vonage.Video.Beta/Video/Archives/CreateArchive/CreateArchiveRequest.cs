using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Common.Validation;
using Vonage.Video.Beta.Video.Archives.Common;

namespace Vonage.Video.Beta.Video.Archives.CreateArchive;

/// <summary>
///     Represents a request to creating an archive.
/// </summary>
public readonly struct CreateArchiveRequest : IVideoRequest
{
    /// <summary>
    /// </summary>
    /// <param name="layout"></param>
    /// <param name="applicationId"></param>
    /// <param name="sessionId"></param>
    /// <param name="hasAudio"></param>
    /// <param name="hasVideo"></param>
    /// <param name="name"></param>
    /// <param name="outputMode"></param>
    /// <param name="resolution"></param>
    /// <param name="streamMode"></param>
    private CreateArchiveRequest(ArchiveLayout layout, string applicationId, string sessionId, bool hasAudio,
        bool hasVideo, string name, string outputMode, RenderResolution resolution, string streamMode)
    {
        this.Layout = layout;
        this.ApplicationId = applicationId;
        this.SessionId = sessionId;
        this.HasAudio = hasAudio;
        this.HasVideo = hasVideo;
        this.Name = name;
        this.OutputMode = outputMode;
        this.Resolution = resolution;
        this.StreamMode = streamMode;
    }

    /// <summary>
    ///     Represents the archive's layout.
    /// </summary>
    public ArchiveLayout Layout { get; }

    /// <summary>
    ///     The Vonage Application UUID.
    /// </summary>
    [JsonIgnore]
    public string ApplicationId { get; }

    /// <summary>
    ///     The session ID of the Vonage Video session you are working with.
    /// </summary>
    public string SessionId { get; }

    /// <summary>
    ///     Whether the archive will record audio (true, the default) or not (false). If you set both hasAudio and hasVideo to
    ///     false, the call to this method results in an error.
    /// </summary>
    public bool HasAudio { get; }

    /// <summary>
    ///     Whether the archive will record video (true, the default) or not (false). If you set both hasAudio and hasVideo to
    ///     false, the call to this method results in an error.
    /// </summary>
    public bool HasVideo { get; }

    /// <summary>
    ///     The name of the archive (for your own identification).
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Whether all streams in the archive are recorded to a single file ("composed", the default) or to individual files
    ///     ("individual").
    /// </summary>
    public string OutputMode { get; }

    /// <summary>
    ///     The resolution of the archive, either "640x480" (SD landscape, the default), "1280x720" (HD landscape), "1920x1080"
    ///     (FHD landscape), "480x640" (SD portrait), "720x1280" (HD portrait), or "1080x1920" (FHD portrait). You may want to
    ///     use a portrait aspect ratio for archives that include video streams from mobile devices (which often use the
    ///     portrait aspect ratio). This property only applies to composed archives. If you set this property and set the
    ///     outputMode property to "individual", the call to the REST method results in an error.
    /// </summary>
    public RenderResolution Resolution { get; }

    /// <summary>
    ///     Whether streams included in the archive are selected automatically ("auto", the default) or manually ("manual").
    ///     When streams are selected automatically ("auto"), all streams in the session can be included in the archive. When
    ///     streams are selected manually ("manual"), you specify streams to be included based on calls to this REST method.
    ///     You can specify whether a stream's audio, video, or both are included in the archive. In composed archives, in both
    ///     automatic and manual modes, the archive composer includes streams based on stream prioritization rules.
    /// </summary>
    public string StreamMode { get; }

    /// <summary>
    ///     Parses the input into a CreateArchiveRequest.
    /// </summary>
    /// <param name="applicationId">The Vonage Application UUID.</param>
    /// <param name="sessionId">The session ID of the Vonage Video session you are working with.</param>
    /// <param name="hasAudio">
    ///     Whether the archive will record audio (true, the default) or not (false). If you set both
    ///     hasAudio and hasVideo to false, the call to this method results in an error.
    /// </param>
    /// <param name="hasVideo">
    ///     Whether the archive will record video (true, the default) or not (false). If you set both
    ///     hasAudio and hasVideo to false, the call to this method results in an error.
    /// </param>
    /// <param name="name">The name of the archive (for your own identification).</param>
    /// <param name="outputMode">
    ///     Whether all streams in the archive are recorded to a single file ("composed", the default) or
    ///     to individual files ("individual").
    /// </param>
    /// <param name="resolution">
    ///     The resolution of the archive, either "640x480" (SD landscape, the default), "1280x720" (HD
    ///     landscape), "1920x1080" (FHD landscape), "480x640" (SD portrait), "720x1280" (HD portrait), or "1080x1920" (FHD
    ///     portrait). You may want to use a portrait aspect ratio for archives that include video streams from mobile devices
    ///     (which often use the portrait aspect ratio). This property only applies to composed archives. If you set this
    ///     property and set the outputMode property to "individual", the call to the REST method results in an error.
    /// </param>
    /// <param name="streamMode">
    ///     Whether streams included in the archive are selected automatically ("auto", the default) or
    ///     manually ("manual"). When streams are selected automatically ("auto"), all streams in the session can be included
    ///     in the archive. When streams are selected manually ("manual"), you specify streams to be included based on calls to
    ///     this REST method. You can specify whether a stream's audio, video, or both are included in the archive. In composed
    ///     archives, in both automatic and manual modes, the archive composer includes streams based on stream prioritization
    ///     rules.
    /// </param>
    /// <param name="layout">The archive's layout.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<CreateArchiveRequest> Parse(
        string applicationId,
        string sessionId,
        bool hasAudio = true,
        bool hasVideo = true,
        string name = "",
        string outputMode = "composed",
        RenderResolution resolution = RenderResolution.StandardDefinitionLandscape,
        string streamMode = "auto",
        ArchiveLayout layout = default) =>
        Result<CreateArchiveRequest>
            .FromSuccess(new CreateArchiveRequest(layout, applicationId, sessionId, hasAudio, hasVideo, name,
                outputMode, resolution, streamMode))
            .Bind(VerifyApplicationId)
            .Bind(VerifySessionId);

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/archive";

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage(string token)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, this.GetEndpointPath());
        httpRequest.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        httpRequest.Content = new StringContent(new JsonSerializer().SerializeObject(this), Encoding.UTF8,
            "application/json");
        return httpRequest;
    }

    private static Result<CreateArchiveRequest> VerifyApplicationId(CreateArchiveRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<CreateArchiveRequest> VerifySessionId(CreateArchiveRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(SessionId));
}