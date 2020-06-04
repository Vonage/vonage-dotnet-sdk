using Newtonsoft.Json;
using System;

namespace Nexmo.Api.Voice.Nccos
{
    [Obsolete("This is made obsolete by the new MultiInputAction type and the MultiInput event see: https://developer.nexmo.com/voice/voice-api/ncco-reference#input for more details")]
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

        public InputAction()
        {
            Action = ActionType.input;
        }
    }
}
