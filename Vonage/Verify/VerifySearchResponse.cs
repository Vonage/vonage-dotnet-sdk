using Newtonsoft.Json;

namespace Vonage.Verify;

/// <summary>
///     Represents the detailed status and history of a verification request.
/// </summary>
public class VerifySearchResponse
{
    /// <summary>
    ///     The Vonage account ID that initiated the verification request.
    /// </summary>
    [JsonProperty("account_id")]
    public string AccountId { get; set; }

    /// <summary>
    ///     The list of PIN code check attempts made against this verification request.
    /// </summary>
    [JsonProperty("checks")]
    public VerifyCheck[] Checks { get; set; }

    /// <summary>
    ///     The three-letter currency code (ISO 4217) for the price values.
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    ///     The date and time when the verification was completed or expired (format: YYYY-MM-DD HH:MM:SS).
    /// </summary>
    [JsonProperty("date_finalized")]
    public string DateFinalized { get; set; }

    /// <summary>
    ///     The date and time when the verification request was submitted (format: YYYY-MM-DD HH:MM:SS).
    /// </summary>
    [JsonProperty("date_submitted")]
    public string DateSubmitted { get; set; }

    /// <summary>
    ///     The estimated cost in EUR of all messages and calls sent during verification. The total cost is the sum of this and <see cref="Price"/>.
    /// </summary>
    [JsonProperty("estimated_price_messages_sent")]
    public string EstimatedPriceMessagesSent { get; set; }

    /// <summary>
    ///     The list of delivery events (SMS or voice calls) that occurred during this verification.
    /// </summary>
    [JsonProperty("events")]
    public VerifyEvent[] Events { get; set; }

    /// <summary>
    ///     The date and time of the first delivery event (format: YYYY-MM-DD HH:MM:SS).
    /// </summary>
    [JsonProperty("first_event_date")]
    public string FirstEventDate { get; set; }

    /// <summary>
    ///     The date and time of the most recent delivery event (format: YYYY-MM-DD HH:MM:SS).
    /// </summary>
    [JsonProperty("last_event_date")]
    public string LastEventDate { get; set; }

    /// <summary>
    ///     The phone number that was verified.
    /// </summary>
    [JsonProperty("number")]
    public string Number { get; set; }

    /// <summary>
    ///     The cost incurred for this verification request, in the currency specified by <see cref="Currency"/>.
    /// </summary>
    [JsonProperty("price")]
    public string Price { get; set; }

    /// <summary>
    ///     The unique identifier of the verification request.
    /// </summary>
    [JsonProperty("request_id")]
    public string RequestId { get; set; }

    /// <summary>
    ///     The sender ID used for SMS messages in this verification.
    /// </summary>
    [JsonProperty("sender_id")]
    public string SenderId { get; set; }

    /// <summary>
    ///     The current status of the verification request (e.g., "IN PROGRESS", "SUCCESS", "FAILED", "EXPIRED").
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }
}