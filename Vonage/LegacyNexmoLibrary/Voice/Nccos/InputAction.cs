using Newtonsoft.Json;
using System;

namespace Nexmo.Api.Voice.Nccos
{
    [Obsolete("This is made obsolete by the new MultiInputAction type and the MultiInput event see: https://developer.nexmo.com/voice/voice-api/ncco-reference#input for more details")]
    public class InputAction : NccoAction
    {
        /// <summary>
        /// The result of the callee's activity is sent to the eventUrl webhook endpoint 
        /// timeOut seconds after the last action. The default value is 3. Max is 10.
        /// </summary>
        [JsonProperty("timeOut")]
        public string TimeOut { get; set; }

        /// <summary>
        /// The number of digits the user can press. The maximum value is 20, the default is 4 digits.
        /// </summary>
        [JsonProperty("maxDigits")]
        public int? MaxDigits { get; set; }

        /// <summary>
        /// Set to true so the callee's activity is sent to your webhook endpoint at eventUrl after they press #. 
        /// If # is not pressed the result is submitted after timeOut seconds. 
        /// The default value is false. 
        /// That is, the result is sent to your webhook endpoint after timeOut seconds.
        /// </summary>
        [JsonProperty("submitOnHash")]
        public string SubmitOnHash { get; set; }

        /// <summary>
        /// Nexmo sends the digits pressed by the callee to this URL after timeOut pause in activity or when # is pressed.
        /// </summary>
        [JsonProperty("eventUrl")]
        public string[] EventUrl { get; set; }

        /// <summary>
        /// The HTTP method used to send event information to event_url The default value is POST.
        /// </summary>
        [JsonProperty("eventMethod")]
        public string EventMethod { get; set; }

        public InputAction()
        {
            Action = ActionType.input;
        }
    }
}
