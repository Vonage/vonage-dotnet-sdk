using Newtonsoft.Json;

namespace Vonage.Voice.EventWebhooks;

public class MultiInput : Input
{
    /// <summary>
    /// Result of Dtmf input
    /// </summary>
    [JsonProperty("dtmf")]
    public new DtmfResult Dtmf { get; set; }

    /// <summary>
    /// Result of the speech recognition
    /// </summary>
    [JsonProperty("speech")]
    public SpeechResult Speech { get; set; }
}