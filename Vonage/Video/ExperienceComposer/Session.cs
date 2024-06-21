using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;
using Vonage.Server;

namespace Vonage.Video.ExperienceComposer;

/// <summary>
///     Represents an Experience Composer session.
/// </summary>
/// <param name="Id">The unique ID for the Experience Composer.</param>
/// <param name="SessionId">The session ID of the Vonage Video session you are working with</param>
/// <param name="ApplicationId">A Vonage Application ID</param>
/// <param name="CreatedAt">
///     The time the Experience Composer started, expressed in milliseconds since the Unix epoch
///     (January 1, 1970, 00:00:00 UTC).
/// </param>
/// <param name="CallbackUrl">The callback URL for Experience Composer events (if one was set).</param>
/// <param name="UpdatedAt">
///     This is the UNIX timestamp when the Experience Composer status was last updated. For this start
///     method, this timestamp matches the createdAt timestamp.
/// </param>
/// <param name="Name">The name of the composed output stream which is published to the session.</param>
/// <param name="Url">
///     A publicly reachable URL controlled by the customer and capable of generating the content to be
///     rendered without user intervention.
/// </param>
/// <param name="Resolution">
///     The resolution of the archive, either "640x480" (SD landscape, the default), "1280x720" (HD
///     landscape), "1920x1080" (FHD landscape), "480x640" (SD portrait), "720x1280" (HD portrait), or "1080x1920" (FHD
///     portrait). You may want to use a portrait aspect ratio for archives that include video streams from mobile devices
///     (which often use the portrait aspect ratio). This property only applies to composed archives. If you set this
///     property and set the outputMode property to "individual", the call to the REST method results in an error.
/// </param>
/// <param name="Status">The session status.</param>
/// <param name="StreamId">The ID of the composed stream being published.</param>
/// <param name="Reason">
///     The reason field is only available when the status is either "stopped" or "failed". If the status
///     is stopped, the reason field will contain either "Max Duration Exceeded" or "Stop Requested." If the status is
///     failed, the reason will contain a more specific error message.
/// </param>
public record Session(
    string Id,
    string SessionId,
    Guid ApplicationId,
    long CreatedAt,
    Uri CallbackUrl,
    long UpdatedAt,
    string Name,
    Uri Url,
    [property: JsonConverter(typeof(EnumDescriptionJsonConverter<RenderResolution>))]
    RenderResolution Resolution,
    [property: JsonConverter(typeof(EnumDescriptionJsonConverter<SessionStatus>))]
    SessionStatus Status,
    string StreamId,
    string Reason);

/// <summary>
/// </summary>
public enum SessionStatus
{
    /// <summary>
    /// </summary>
    [Description("starting")] Starting,

    /// <summary>
    /// </summary>
    [Description("started")] Started,

    /// <summary>
    /// </summary>
    [Description("stopped")] Stopped,

    /// <summary>
    /// </summary>
    [Description("failed")] Failed,
}