using Newtonsoft.Json;

namespace Vonage.Accounts;

/// <summary>
///     Represents the response from updating account settings, including the current webhook URLs and rate limits.
/// </summary>
public class AccountSettingsResult
{
    /// <summary>
    ///     The current or updated webhook URL for inbound SMS messages (MO - Mobile Originated).
    /// </summary>
    [JsonProperty("mo-callback-url")]
    public string MoCallbackUrl { get; set; }

    /// <summary>
    ///     The current or updated webhook URL for delivery receipts (DR - Delivery Receipt).
    /// </summary>
    [JsonProperty("dr-callback-url")]
    public string DrCallbackurl { get; set; }

    /// <summary>
    ///     The maximum number of outbound messages allowed per second.
    /// </summary>
    [JsonProperty("max-outbound-request")]
    public decimal MaxOutboundRequest { get; set; }

    /// <summary>
    ///     The maximum number of inbound messages allowed per second.
    /// </summary>
    [JsonProperty("max-inbound-request")]
    public decimal MaxInboundRequest { get; set; }

    /// <summary>
    ///     The maximum number of API calls allowed per second.
    /// </summary>
    [JsonProperty("max-calls-per-second")]
    public decimal MaxCallsPerSecond { get; set; }
}