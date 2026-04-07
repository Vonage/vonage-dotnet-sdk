using Newtonsoft.Json;

namespace Vonage.ShortCodes;

/// <summary>
///     Represents the opt-in/opt-out status record for a phone number subscribed to a short code.
/// </summary>
public class OptInRecord
{
    /// <summary>
    ///     Gets or sets the phone number in E.164 format (e.g., "14155551234").
    /// </summary>
    [JsonProperty("msisdn")]
    public string Msisdn { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the phone number has opted in to receive messages.
    /// </summary>
    [JsonProperty("opt-in")]
    public bool OptIn { get; set; }

    /// <summary>
    ///     Gets or sets the date when the phone number opted in.
    /// </summary>
    [JsonProperty("opt-in-date")]
    public string OptInDate { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the phone number has opted out from receiving messages.
    /// </summary>
    [JsonProperty("opt-out")]
    public bool OptOut { get; set; }

    /// <summary>
    ///     Gets or sets the date when the phone number opted out.
    /// </summary>
    [JsonProperty("opt-out-date")]
    public string OptOutDate { get; set; }
}