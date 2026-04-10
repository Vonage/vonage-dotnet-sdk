using System;
using System.ComponentModel;

namespace Vonage.Video.Broadcast;

/// <summary>
///     Represents a broadcast.
/// </summary>
public struct Broadcast
{
    /// <summary>
    ///     The Vonage Application UUID.
    /// </summary>
    public Guid ApplicationId { get; set; }

    /// <summary>
    ///     An object containing details about the HLS and RTMP broadcasts.
    /// </summary>
    public BroadcastUrl BroadcastUrls { get; set; }

    /// <summary>
    ///     The time the broadcast started, expressed in milliseconds since the Unix epoch (January 1, 1970, 00:00:00 UTC).
    /// </summary>
    public long CreatedAt { get; set; }

    /// <summary>
    ///     Whether the broadcast has audio.
    /// </summary>
    public bool HasAudio { get; set; }

    /// <summary>
    ///     Whether the broadcast has video.
    /// </summary>
    public bool HasVideo { get; set; }

    /// <summary>
    ///     The unique ID for the broadcast.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     The maximum bitrate for the broadcast stream, in bits per second.
    /// </summary>
    public int MaxBitrate { get; set; }

    /// <summary>
    ///     The maximum duration for the broadcast (if one was set), in seconds.
    /// </summary>
    public int MaxDuration { get; set; }

    /// <summary>
    ///     The unique tag for simultaneous broadcasts (if one was set).
    /// </summary>
    public string MultiBroadcastTag { get; set; }

    /// <summary>
    ///     The resolution of the broadcast, either "640x480" (SD landscape, the default), "1280x720" (HD landscape),
    ///     "1920x1080" (FHD landscape), "480x640" (SD portrait), "720x1280" (HD portrait), or "1080x1920" (FHD portrait).
    ///     You may want to use a portrait aspect ratio for broadcasts that include video streams from mobile devices (which
    ///     often use the portrait aspect ratio).
    /// </summary>
    public string Resolution { get; set; }

    /// <summary>
    ///     The Vonage Video session ID.
    /// </summary>
    public string SessionId { get; set; }

    /// <summary>
    ///     The settings for the broadcast, including HLS configuration.
    /// </summary>
    public BroadcastSettings Settings { get; set; }

    /// <summary>
    ///     Current status of the broadcast.
    /// </summary>
    public BroadcastStatus Status { get; set; }

    /// <summary>
    ///     Whether streams included in the broadcast are selected automatically ("auto", the default) or manually ("manual").
    ///     When streams are selected automatically ("auto"), all streams in the session can be included in the broadcast. When
    ///     streams are selected manually ("manual"), you specify streams to be included based on calls to this REST method.
    ///     You can specify whether a stream's audio, video, or both are included in the broadcast.
    /// </summary>
    public string StreamMode { get; set; }

    /// <summary>
    ///     An array of objects corresponding to streams currently being broadcast. This is only set for a broadcast with the
    ///     status set to "started" and the streamMode set to "manual"
    /// </summary>
    public LiveStream[] Streams { get; set; }

    /// <summary>
    ///     For this start method, this timestamp matches the createdAt timestamp.
    /// </summary>
    public long UpdatedAt { get; set; }

    /// <summary>
    ///     Represents the status of a broadcast.
    /// </summary>
    public enum BroadcastStatus
    {
        /// <summary>
        ///     Indicates the broadcast is started.
        /// </summary>
        [Description("started")] Started,

        /// <summary>
        ///     Indicates the broadcast is stopped.
        /// </summary>
        [Description("stopped")] Stopped,
    }

    /// <summary>
    ///     Represents information regarding HLS and RTMP broadcasts.
    /// </summary>
    /// <param name="Hls">
    ///     If you specified an HLS endpoint, the object includes an hls property, which is set to the URL for the HLS
    ///     broadcast. Note this HLS broadcast URL points to an index file, an .M3U8- formatted playlist that contains a list
    ///     of URLs to .ts media segment files (MPEG-2 transport stream files). While the URLs of both the playlist index file
    ///     and media segment files are provided as soon as the HTTP response is returned, these URLs should not be accessed
    ///     until 15 – 20 seconds later, after the initiation of the HLS broadcast, due to the delay between the HLS broadcast
    ///     and the live streams in the Vonage Video session. See
    ///     https://developer.apple.com/library/ios/technotes/tn2288/_index.html for more information about the playlist index
    ///     file and media segment files for HLS.
    /// </param>
    /// <param name="Rtmp">
    ///     If you specified RTMP stream endpoints, the object includes an rtmp property. This is an array of objects that
    ///     include information on each of the RTMP streams. Each of these objects has the following properties: id (the ID you
    ///     assigned to the RTMP stream),serverUrl (the server URL), streamName (the stream name), and status property (which
    ///     is set to "connecting"). You can call the Vonage Video REST method to check for status updates for the broadcast.
    /// </param>
    public record BroadcastUrl(Uri Hls, RtmpStream[] Rtmp);

    /// <summary>
    ///     Represents a RtmpStream.
    /// </summary>
    /// <param name="Id">The stream Id.</param>
    /// <param name="StreamName">The stream name.</param>
    /// <param name="ServerUrl">The server url.</param>
    /// <param name="Status">The stream status.</param>
    public record RtmpStream(Guid Id, string StreamName, Uri ServerUrl, RtmpStatus Status);

    /// <summary>
    ///     The status of the RTMP stream. Poll frequently to check status updates.
    /// </summary>
    public enum RtmpStatus
    {
        /// <summary>
        ///     Indicates the platform is in the process of connecting to the remote RTMP server. This is the initial state, and it
        ///     is the status if you start when there are no streams published in the session. It changes to "live" when there are
        ///     streams (or it changes to one of the other states).
        /// </summary>
        [Description("connecting")] Connecting,

        /// <summary>
        ///     Indicates platform has successfully connected to the remote RTMP server, and the media is streaming.
        /// </summary>
        [Description("live")] Live,

        /// <summary>
        ///     Indicates platform could not connect to the remote RTMP server. This is due to an unreachable server or an error in
        ///     the RTMP handshake. Causes include rejected RTMP connections, non-existing RTMP applications, rejected stream
        ///     names, authentication errors, etc. Check that the server is online, and that you have provided the correct server
        ///     URL and stream name.
        /// </summary>
        [Description("offline")] Offline,

        /// <summary>
        ///     Indicates there is an error in the platform.
        /// </summary>
        [Description("error")] Error,
    }

    /// <summary>
    ///     Represents the settings for a broadcast.
    /// </summary>
    /// <param name="Hls">The HLS-specific settings for the broadcast.</param>
    public record BroadcastSettings(HlsSettings Hls);

    /// <summary>
    ///     Represents HLS-specific settings.
    /// </summary>
    /// <param name="Dvr">
    ///     Whether to enable DVR functionality (rewinding, pausing, and resuming) in players that support it.
    /// </param>
    /// <param name="LowLatency">
    ///     Whether to enable low-latency mode for the HLS stream. This feature is incompatible with DVR mode.
    /// </param>
    public record HlsSettings(bool Dvr, bool LowLatency);

    /// <summary>
    ///     Represents a stream currently being broadcast.
    /// </summary>
    /// <param name="StreamId"> The stream ID of the stream included in the broadcast.</param>
    /// <param name="HasAudio">  Whether the stream's audio is included in the broadcast.</param>
    /// <param name="HasVideo">  Whether the stream's video is included in the broadcast.</param>
    public record LiveStream(Guid StreamId, bool HasAudio, bool HasVideo);
}