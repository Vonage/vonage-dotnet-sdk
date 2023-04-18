using Newtonsoft.Json;

namespace Vonage.Voice.EventWebhooks;

public class DtmfResult
{
    /// <summary>
    /// the dtmf digits input by the user
    /// </summary>
    [JsonProperty("digits")]
    public string Digits { get; set; }

    /// <summary>
    /// indicates whether the dtmf input timed out
    /// </summary>
    [JsonProperty("timed_out")]
    public bool TimedOut { get; set; }
}