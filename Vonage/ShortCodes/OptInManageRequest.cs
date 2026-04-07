using Newtonsoft.Json;

namespace Vonage.ShortCodes;

/// <summary>
///     Represents a request to manage the opt-in status for a phone number.
/// </summary>
public class OptInManageRequest
{
    /// <summary>
    ///     Gets or sets the phone number to manage opt-in status for, in E.164 format (e.g., "14155551234").
    /// </summary>
    [JsonProperty("msisdn")]
    public string Msisdn { get; set; }
}