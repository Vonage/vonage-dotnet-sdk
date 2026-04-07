using Newtonsoft.Json;

namespace Vonage.Verify;

/// <summary>
///     Represents the response from validating a PIN code, indicating whether the code was correct and the cost incurred.
/// </summary>
public class VerifyCheckResponse : VerifyResponseBase
{
    /// <summary>
    ///     The unique identifier of the verification request that was checked.
    /// </summary>
    [JsonProperty("request_id")]
    public string RequestId { get; set; }

    /// <summary>
    ///     The identifier of the specific verification event (SMS or voice call) that delivered the code.
    /// </summary>
    [JsonProperty("event_id")]
    public string EventId { get; set; }

    /// <summary>
    ///     The cost incurred for this verification check, in the currency specified by <see cref="Currency"/>.
    /// </summary>
    [JsonProperty("price")]
    public string Price { get; set; }

    /// <summary>
    ///     The three-letter currency code (ISO 4217) for the <see cref="Price"/> value.
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    ///     The estimated cost in EUR of all messages and calls sent during the verification process. This value may update after the request completes. The total cost is the sum of this field and <see cref="Price"/>. May not be present depending on your pricing model.
    /// </summary>
    [JsonProperty("estimated_price_messages_sent")]
    public string EstimatedPriceMessagesSent { get; set; }
}