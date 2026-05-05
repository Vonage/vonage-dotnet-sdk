using System.Text.Json.Serialization;

namespace Vonage.Applications.Capabilities;

/// <summary>
///     Represents the webhook endpoints for the Verify capability.
///     The status webhook is POST-only as mandated by the Verify API.
/// </summary>
public record VerifyWebhooks
{
    /// <summary>
    ///     The URL Vonage sends Verify status updates to.
    /// </summary>
    [JsonPropertyName("status_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PostOnlyWebhook StatusUrl { get; init; }
}
