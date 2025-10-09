#region
using System.Net.Http;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
#endregion

namespace Vonage.Common;

/// <summary>
/// </summary>
public class Webhook
{
    /// <summary>
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Type
    {
        /// <summary>
        ///     Event URL
        /// </summary>
        [EnumMember(Value = "event_url")] EventUrl = 2,

        /// <summary>
        ///     Inbound URL
        /// </summary>
        [EnumMember(Value = "inbound_url")] InboundUrl = 3,

        /// <summary>
        ///     Status URL
        /// </summary>
        [EnumMember(Value = "status_url")] StatusUrl = 4,

        /// <summary>
        ///     Unknown
        /// </summary>
        [EnumMember(Value = "Unknown")] Unknown = 6,

        /// <summary>
        ///     Room changed
        /// </summary>
        [EnumMember(Value = "room_changed")] RoomChanged = 7,

        /// <summary>
        ///     Session changed
        /// </summary>
        [EnumMember(Value = "session_changed")]
        SessionChanged = 8,

        /// <summary>
        ///     Recording changed
        /// </summary>
        [EnumMember(Value = "recording_changed")]
        RecordingChanged = 9,

        /// <summary>
        ///     Recordings callback URL New recordings alert
        /// </summary>
        [EnumMember(Value = "archive_status")] ArchiveStatus = 10,

        /// <summary>
        ///     Session monitoring Live updates of session events
        /// </summary>
        [EnumMember(Value = "connection_created")]
        ConnectionCreated = 11,

        /// <summary>
        ///     Session monitoring Live updates of session events
        /// </summary>
        [EnumMember(Value = "connection_destroyed")]
        ConnectionDestroyed = 12,

        /// <summary>
        ///     Session monitoring Live updates of session events
        /// </summary>
        [EnumMember(Value = "session_created")]
        SessionCreated = 13,

        /// <summary>
        ///     Session monitoring Live updates of session events
        /// </summary>
        [EnumMember(Value = "session_destroyed")]
        SessionDestroyed = 14,

        /// <summary>
        ///     Session monitoring Live updates of session events
        /// </summary>
        [EnumMember(Value = "session_notification")]
        SessionNotification = 15,

        /// <summary>
        ///     Session monitoring Live updates of session events
        /// </summary>
        [EnumMember(Value = "stream_created")] StreamCreated = 16,

        /// <summary>
        ///     Session monitoring Live updates of session events
        /// </summary>
        [EnumMember(Value = "stream_destroyed")]
        StreamDestroyed = 17,

        /// <summary>
        ///     URL to receive Experience Composer events
        /// </summary>
        [EnumMember(Value = "render_status")] RenderStatus = 18,

        /// <summary>
        ///     Monitoring live-streaming broadcast status changes
        /// </summary>
        [EnumMember(Value = "broadcast_status")]
        BroadcastStatus = 19,

        /// <summary>
        ///     Caption callback URL
        /// </summary>
        [EnumMember(Value = "captions_status")]
        CaptionsStatus = 20,
    }

    /// <summary>
    /// </summary>
    public Webhook()
    {
    }

    /// <summary>
    /// </summary>
    /// <param name="address"></param>
    /// <param name="method"></param>
    public Webhook(string address, HttpMethod method)
    {
        this.Address = address;
        this.Method = method.ToString().ToUpperInvariant();
    }

    /// <summary>
    /// </summary>
    [JsonProperty("address", Order = 1)]
    public string Address { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty("http_method", Order = 0)]
    public string Method { get; set; }
}