using Newtonsoft.Json;

namespace Vonage.ShortCodes;

/// <summary>
///     Represents a request to send an event-based alert message via a pre-approved short code.
/// </summary>
public class AlertRequest
{
    /// <summary>
    ///     Gets or sets the recipient phone number in E.164 format (e.g., "14155551234").
    /// </summary>
    [JsonProperty("to")]
    public string To { get; set; }

    /// <summary>
    ///     Gets or sets whether to request a delivery receipt. Set to "1" to receive delivery status callbacks.
    /// </summary>
    [JsonProperty("status-report-req")]
    public string StatusReportReq { get; set; }

    /// <summary>
    ///     Gets or sets the client reference. This user-defined value is included in delivery receipts for tracking purposes;
    ///     limited to 40 characters.
    /// </summary>
    [JsonProperty("client-ref")]
    public string ClientRef { get; set; }

    /// <summary>
    ///     Gets or sets the pre-approved message template with optional placeholders (e.g., "Your code is {pin}").
    /// </summary>
    [JsonProperty("template")]
    public string Template { get; set; }

    /// <summary>
    ///     Gets or sets the message encoding type. Use "text" for standard GSM characters or "unicode" for non-GSM characters.
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    ///     Gets or sets custom key-value pairs to substitute into the template placeholders.
    /// </summary>
    [JsonProperty("custom")]
    public object Custom { get; set; }
}