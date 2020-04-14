using Newtonsoft.Json;

namespace Nexmo.Api.Voice.Nccos
{
    public class InputAction : NccoAction
    {
        [JsonProperty("timeOut")]
        public string TimeOut { get; set; }

        [JsonProperty("maxDigits")]
        public int? MaxDigits { get; set; }

        [JsonProperty("submitOnHash")]
        public string SubmitOnHash { get; set; }

        [JsonProperty("eventUrl")]
        public string[] EventUrl { get; set; }

        [JsonProperty("eventMethod")]
        public string EventMethod { get; set; }

        public InputAction()
        {
            Action = ActionType.input;
        }
    }
}
