using System.Text.Json.Serialization;

namespace Vonage.Applications.Capabilities;

/// <summary>
///     Represents a POST-only webhook endpoint. The HTTP method is always POST and cannot be changed.
///     Use this type for webhooks where the API mandates POST (Messages, Verify, Meetings).
/// </summary>
public record PostOnlyWebhook(
    [property: JsonPropertyName("address")] string Address)
{
    /// <summary>Always "POST"; exposed in JSON to satisfy the wire contract.</summary>
    [JsonPropertyName("http_method")]
    public string Method => "POST";
}
