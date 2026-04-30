using System.Text.Json.Serialization;

namespace Vonage.Applications.Capabilities;

/// <summary>
///     Represents the Verify v2 capability configuration for an application.
/// </summary>
public record VerifyCapability
{
    /// <summary>
    ///     Webhook endpoints for Verify events.
    /// </summary>
    [JsonPropertyName("webhooks")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VerifyWebhooks Webhooks { get; init; }
}
