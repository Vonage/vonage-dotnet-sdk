#region
using System.Net.Http;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
#endregion

namespace Vonage.Common;

/// <summary>
///     Represents a webhook endpoint configuration for receiving event callbacks from Vonage APIs.
/// </summary>
/// <remarks>
///     <para>Webhooks allow your application to receive real-time notifications about events such as
///     message delivery status, incoming calls, and session changes.</para>
///     <para>Configure the webhook address and HTTP method to receive callbacks.</para>
/// </remarks>
public class Webhook
{
    /// <summary>
    ///     Defines the type of webhook event.
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
    ///     Initializes a new instance of the <see cref="Webhook"/> class.
    /// </summary>
    public Webhook()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Webhook"/> class with the specified address and HTTP method.
    /// </summary>
    /// <param name="address">The URL endpoint where webhook events will be sent.</param>
    /// <param name="method">The HTTP method to use when sending webhook events (typically GET or POST).</param>
    /// <example>
    /// <code><![CDATA[
    /// var webhook = new Webhook("https://example.com/webhooks/status", HttpMethod.Post);
    /// ]]></code>
    /// </example>
    public Webhook(string address, HttpMethod method)
    {
        this.Address = address;
        this.Method = method.ToString().ToUpperInvariant();
    }

    /// <summary>
    ///     Gets or sets the URL endpoint where webhook events will be sent.
    /// </summary>
    [JsonProperty("address", Order = 1)]
    public string Address { get; set; }

    /// <summary>
    ///     Gets or sets the HTTP method to use when sending webhook events (e.g., "GET" or "POST").
    /// </summary>
    [JsonProperty("http_method", Order = 0)]
    public string Method { get; set; }
}