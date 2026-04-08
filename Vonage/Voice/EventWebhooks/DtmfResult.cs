#region
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
///     Represents the result of DTMF input collected from a caller during an input action.
/// </summary>
public class DtmfResult
{
    /// <summary>
    ///     The DTMF digits entered by the caller. May include 0-9, *, and #.
    /// </summary>
    [JsonProperty("digits")]
    [JsonPropertyName("digits")]
    public string Digits { get; set; }

    /// <summary>
    ///     Indicates whether the DTMF input timed out before the caller finished entering digits.
    /// </summary>
    [JsonProperty("timed_out")]
    [JsonPropertyName("timed_out")]
    public bool TimedOut { get; set; }
}