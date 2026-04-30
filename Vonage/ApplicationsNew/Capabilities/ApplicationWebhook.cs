using System.Text.Json.Serialization;
using Vonage.Common.Serialization;

namespace Vonage.ApplicationsNew.Capabilities;

/// <summary>
///     Represents a webhook endpoint with an address and delivery method. Supports GET or POST.
/// </summary>
public record ApplicationWebhook(
    [property: JsonPropertyName("address")] string Address,
    [property: JsonPropertyName("http_method")]
    [property: JsonConverter(typeof(EnumDescriptionJsonConverter<WebhookMethod>))]
    WebhookMethod Method);
