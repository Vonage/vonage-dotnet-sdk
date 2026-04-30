using System.Text.Json.Serialization;

namespace Vonage.ApplicationsNew.Capabilities;

/// <summary>
///     Represents the Meetings capability configuration for an application.
/// </summary>
public record MeetingsCapability
{
    /// <summary>
    ///     Webhook endpoints for Meetings events.
    /// </summary>
    [JsonPropertyName("webhooks")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MeetingsWebhooks Webhooks { get; init; }
}
