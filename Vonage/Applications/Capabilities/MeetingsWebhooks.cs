using System.Text.Json.Serialization;

namespace Vonage.Applications.Capabilities;

/// <summary>
///     Represents the webhook endpoints for the Meetings capability.
///     All webhooks are POST-only as mandated by the Meetings API.
/// </summary>
public record MeetingsWebhooks
{
    /// <summary>
    ///     The URL Vonage forwards recording status changes to.
    /// </summary>
    [JsonPropertyName("recording_changed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PostOnlyWebhook RecordingChanged { get; init; }

    /// <summary>
    ///     The URL Vonage forwards meeting room changes to.
    /// </summary>
    [JsonPropertyName("room_changed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PostOnlyWebhook RoomChanged { get; init; }

    /// <summary>
    ///     The URL Vonage forwards session changes to.
    /// </summary>
    [JsonPropertyName("session_changed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PostOnlyWebhook SessionChanged { get; init; }
}
