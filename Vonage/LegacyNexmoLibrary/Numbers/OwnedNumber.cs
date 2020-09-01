using Newtonsoft.Json;

namespace Nexmo.Api.Numbers
{
    [System.Obsolete("The Nexmo.Api.Numbers.OwnedNumber class is obsolete. " +
        "References to it should be switched to the new Vonage.Numbers.OwnedNumber class.")]
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
}