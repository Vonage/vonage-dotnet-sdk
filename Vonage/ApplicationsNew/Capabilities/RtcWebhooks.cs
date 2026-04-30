using System.Text.Json.Serialization;

namespace Vonage.ApplicationsNew.Capabilities;

/// <summary>
///     Represents the webhook endpoints for the RTC capability.
/// </summary>
public record RtcWebhooks
{
    /// <summary>
    ///     The URL Vonage sends RTC events to.
    /// </summary>
    [JsonPropertyName("event_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ApplicationWebhook EventUrl { get; init; }
}
