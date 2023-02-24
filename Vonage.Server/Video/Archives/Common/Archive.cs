using System.Text.Json.Serialization;
using Vonage.Server.Common;

namespace Vonage.Server.Video.Archives.Common;

/// <summary>
///     Represents an archive.
/// </summary>
public struct Archive
{
    /// <summary>
    ///     Your Vonage application ID.
    /// </summary>
    public string ApplicationId { get; }

    /// <summary>
    ///     The timestamp for when the archive started recording, expressed in milliseconds since the Unix epoch (January 1,
    ///     1970, 00:00:00 UTC).
    /// </summary>
    public long CreatedAt { get; }

    /// <summary>
    ///     The duration of the archive in seconds. For archives that have are being recorded (with the status property set to
    ///     "started"), this value is set to 0.
    /// </summary>
    public int Duration { get; }

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
    ///     The unique archive ID.
    /// </summary>
    public string Id { get; }

    /// <summary>
    ///     The name of the archive (for your own identification).
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     For archives with the status "stopped", this can be set to "maximum duration exceeded", "maximum idle time
    ///     exceeded", "session ended", "user initiated". For archives with the status "failed", this can be set to "failure".
    /// </summary>
    public string Reason { get; }

    /// <summary>
    ///     The resolution of the archive, either "640x480" (SD landscape, the default), "1280x720" (HD landscape), "1920x1080"
    ///     (FHD landscape), "480x640" (SD portrait), "720x1280" (HD portrait), or "1080x1920" (FHD portrait). You may want to
    ///     use a portrait aspect ratio for archives that include video streams from mobile devices (which often use the
    ///     portrait aspect ratio). This property only applies to composed archives. If you set this property and set the
    ///     outputMode property to "individual", the call to the REST method results in an error.
    /// </summary>
    public RenderResolution Resolution { get; }

    /// <summary>
    ///     The session ID of the Vonage Video session you are working with.
    /// </summary>
    public string SessionId { get; }

    /// <summary>
    ///     The size of the archive file. For archives that have not been generated, this value is set to 0.
    /// </summary>
    public int Size { get; }

    /// <summary>
    ///     The status of the archive:
    ///     "available" — The archive is available for download from the Vonage Video cloud.
    ///     "expired" — The archive is no longer available for download from the Vonage Video cloud.
    ///     "failed" — The archive recording failed.
    ///     "paused" — When an archive is paused, nothing is recorded. The archive is paused if any of the following conditions
    ///     occur:
    ///     No clients are publishing streams to the session. In this case, there is a timeout of 60 minutes, after which the
    ///     archive stops and the archive status changes to "stopped".
    ///     All clients disconnect the session. After 60 seconds the archive stops and the archive status changes to "stopped".
    ///     If a client resumes publishing while the archive is in the "paused" state, the archive recording resumes and the
    ///     status changes back to "started".
    ///     "started" — The archive started and is in the process of being recorded.
    ///     "stopped" — The archive stopped recording.
    ///     "uploaded" — The archive is available for download from the S3 bucket you specified in your Video API account.
    /// </summary>
    public string Status { get; }

    /// <summary>
    ///     Whether streams included in the archive are selected automatically ("auto", the default) or manually ("manual").
    ///     When streams are selected automatically ("auto"), all streams in the session can be included in the archive. When
    ///     streams are selected manually ("manual"), you specify streams to be included based on calls to this REST method.
    ///     You can specify whether a stream's audio, video, or both are included in the archive. In composed archives, in both
    ///     automatic and manual modes, the archive composer includes streams based on stream prioritization rules.
    /// </summary>
    public string StreamMode { get; }

    /// <summary>
    ///     The collection of streams.
    /// </summary>
    public Stream[] Streams { get; }

    /// <summary>
    ///     The download URL of the available archive file. This is only set for an archive with the status set to "available";
    ///     for other archives, (including archives with the status "uploaded") this property is set to null. The download URL
    ///     is obfuscated, and the file is only available from the URL for 10 minutes.
    /// </summary>
    public string Url { get; }

    /// <summary>
    ///     Creates an archive.
    /// </summary>
    /// <param name="createdAt">
    ///     The timestamp for when the archive started recording, expressed in milliseconds since the Unix
    ///     epoch (January 1, 1970, 00:00:00 UTC).
    /// </param>
    /// <param name="duration">
    ///     The duration of the archive in seconds. For archives that have are being recorded (with the
    ///     status property set to "started"), this value is set to 0.
    /// </param>
    /// <param name="hasAudio">
    ///     Whether the archive will record audio (true, the default) or not (false). If you set both
    ///     hasAudio and hasVideo to false, the call to this method results in an error.
    /// </param>
    /// <param name="hasVideo">
    ///     Whether the archive will record video (true, the default) or not (false). If you set both
    ///     hasAudio and hasVideo to false, the call to this method results in an error.
    /// </param>
    /// <param name="id">The unique archive ID.</param>
    /// <param name="name">The name of the archive (for your own identification)</param>
    /// <param name="applicationId">Your Vonage application ID</param>
    /// <param name="reason">
    ///     For archives with the status "stopped", this can be set to "maximum duration exceeded", "maximum
    ///     idle time exceeded", "session ended", "user initiated". For archives with the status "failed", this can be set to
    ///     "failure".
    /// </param>
    /// <param name="resolution">
    ///     The resolution of the archive, either "640x480" (SD landscape, the default), "1280x720" (HD
    ///     landscape), "1920x1080" (FHD landscape), "480x640" (SD portrait), "720x1280" (HD portrait), or "1080x1920" (FHD
    ///     portrait). You may want to use a portrait aspect ratio for archives that include video streams from mobile devices
    ///     (which often use the portrait aspect ratio). This property only applies to composed archives. If you set this
    ///     property and set the outputMode property to "individual", the call to the REST method results in an error.
    /// </param>
    /// <param name="sessionId">The session ID of the Vonage Video session you are working with</param>
    /// <param name="size">The size of the archive file. For archives that have not been generated, this value is set to 0.</param>
    /// <param name="status">The status of the archive.</param>
    /// <param name="streamMode">
    ///     Whether streams included in the archive are selected automatically ("auto", the default) or
    ///     manually ("manual"). When streams are selected automatically ("auto"), all streams in the session can be included
    ///     in the archive. When streams are selected manually ("manual"), you specify streams to be included based on calls to
    ///     this REST method. You can specify whether a stream's audio, video, or both are included in the archive. In composed
    ///     archives, in both automatic and manual modes, the archive composer includes streams based on stream prioritization
    ///     rules.
    /// </param>
    /// <param name="url">
    ///     The download URL of the available archive file. This is only set for an archive with the status set
    ///     to "available"; for other archives, (including archives with the status "uploaded") this property is set to null.
    ///     The download URL is obfuscated, and the file is only available from the URL for 10 minutes.
    /// </param>
    /// <param name="streams">The collection of streams.</param>
    [JsonConstructor]
    public Archive(long createdAt, int duration, bool hasAudio, bool hasVideo, string id, string name,
        string applicationId, string reason, RenderResolution resolution, string sessionId, int size, string status,
        string streamMode, string url, Stream[] streams)
    {
        this.CreatedAt = createdAt;
        this.Duration = duration;
        this.HasAudio = hasAudio;
        this.HasVideo = hasVideo;
        this.Id = id;
        this.Name = name;
        this.ApplicationId = applicationId;
        this.Reason = reason;
        this.Resolution = resolution;
        this.SessionId = sessionId;
        this.Size = size;
        this.Status = status;
        this.StreamMode = streamMode;
        this.Url = url;
        this.Streams = streams;
    }

    /// <summary>
    ///     Represents a stream.
    /// </summary>
    public struct Stream
    {
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
        ///     The stream ID of the stream included in the archive.
        /// </summary>
        public string StreamId { get; }

        /// <summary>
        ///     Creates a stream.
        /// </summary>
        /// <param name="streamId">The stream ID of the stream included in the archive.</param>
        /// <param name="hasAudio">
        ///     Whether the archive will record audio (true, the default) or not (false). If you set both
        ///     hasAudio and hasVideo to false, the call to this method results in an error.
        /// </param>
        /// <param name="hasVideo">
        ///     Whether the archive will record video (true, the default) or not (false). If you set both
        ///     hasAudio and hasVideo to false, the call to this method results in an error.
        /// </param>
        [JsonConstructor]
        public Stream(string streamId, bool hasAudio, bool hasVideo)
        {
            this.StreamId = streamId;
            this.HasAudio = hasAudio;
            this.HasVideo = hasVideo;
        }
    }
}