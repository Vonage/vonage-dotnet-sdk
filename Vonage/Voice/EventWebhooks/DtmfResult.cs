#region
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
/// </summary>
public class DtmfResult
{
    /// <summary>
    /// the dtmf digits input by the user
    /// </summary>
    [JsonProperty("digits")]
    [JsonPropertyName("digits")]
    public string Digits { get; set; }

    /// <summary>
    /// indicates whether the dtmf input timed out
    /// </summary>
    [JsonProperty("timed_out")]
    [JsonPropertyName("timed_out")]
    public bool TimedOut { get; set; }
}