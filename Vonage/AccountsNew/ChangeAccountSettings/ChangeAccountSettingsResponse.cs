using System.Text.Json.Serialization;

namespace Vonage.AccountsNew.ChangeAccountSettings;

/// <summary>
///     Represents the current account settings returned after a change-settings request.
/// </summary>
public record ChangeAccountSettingsResponse(
    [property: JsonPropertyName("mo-callback-url")] string InboundSmsCallbackUrl,
    [property: JsonPropertyName("dr-callback-url")] string DeliveryReceiptCallbackUrl,
    [property: JsonPropertyName("max-outbound-request")] int MaxOutboundRequest,
    [property: JsonPropertyName("max-inbound-request")] int MaxInboundRequest,
    [property: JsonPropertyName("max-calls-per-second")] int MaxCallsPerSecond);
