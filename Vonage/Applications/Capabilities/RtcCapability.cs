using System.Text.Json.Serialization;

namespace Vonage.Applications.Capabilities;

/// <summary>
///     Represents the RTC (Client SDK) capability configuration for an application.
/// </summary>
public record RtcCapability
{
    /// <summary>
    ///     Webhook endpoints for RTC events.
    /// </summary>
    [JsonPropertyName("webhooks")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public RtcWebhooks Webhooks { get; init; }

    /// <summary>
    ///     Whether Vonage signs incoming webhook requests so the application can verify their authenticity.
    /// </summary>
    [JsonPropertyName("signed_callbacks")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? UseSignedCallbacks { get; init; }
}
