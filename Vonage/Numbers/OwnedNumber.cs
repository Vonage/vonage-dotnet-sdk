using Newtonsoft.Json;

namespace Vonage.Numbers;

public class OwnedNumber : Number
{
    [JsonProperty("moHttpUrl")]
    public string MoHttpUrl { get; set; }

    [JsonProperty("messagesCallbackType")]
    public string MessagesCallbackType { get; set; }

    [JsonProperty("messagesCallbackValue")]
    public string MessagesCallbackValue { get; set; }

    [JsonProperty("voiceCallbackType")]
    public string VoiceCallbackType { get; set; }

    [JsonProperty("voiceCallbackValue")]
    public string VoiceCallbackValue { get; set; }
}