using Newtonsoft.Json;

namespace Nexmo.Api.Voice.Nccos
{
    public class InputAction : NccoAction
    {
        [JsonProperty("timeOut")]
        public string TimeOut { get; set; }

        [JsonProperty("maxDigits")]
        public string MaxDigits { get; set; }

        [JsonProperty("submitOnHash")]
        public string SubmitOnHash { get; set; }

        [JsonProperty("eventUrl")]
        public string[] EventUrl { get; set; }

        [JsonProperty("eventMethod")]
        public string EventMethod { get; set; }

        /// <summary>
        /// Speech recognition settings. Should be specified to enable speech input.
        /// </summary>
        [JsonProperty("speech")]
        public SpeechSettings SpeechSettings { get; set; }

        public InputAction()
        {
            Action = ActionType.input;
        }
    }
}
