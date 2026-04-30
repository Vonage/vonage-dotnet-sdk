using System.Text.Json.Serialization;
using Vonage.Common.Serialization;

namespace Vonage.ApplicationsNew.Capabilities;

/// <summary>
///     Represents a Voice webhook endpoint. Includes optional connection and socket timeouts.
/// </summary>
public record VoiceWebhook(
    [property: JsonPropertyName("address")] string Address,
    [property: JsonPropertyName("http_method")]
    [property: JsonConverter(typeof(EnumDescriptionJsonConverter<WebhookMethod>))]
    WebhookMethod Method,
    [property: JsonPropertyName("connect_timeout")]
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    int? ConnectionTimeout = 1000,
    [property: JsonPropertyName("socket_timeout")]
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    int? SocketTimeout = 500);
