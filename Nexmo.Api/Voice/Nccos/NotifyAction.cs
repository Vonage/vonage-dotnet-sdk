using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nexmo.Api.Voice.Nccos
{
    public class NotifyAction : NccoAction
    {
        [JsonProperty("payload")]
        public object Payload { get; set; }

        [JsonProperty("eventUrl")]
        public string[] EventUrl { get; set; }

        [JsonProperty("eventMethod")]
        public string EventMethod { get; set; }

        public NotifyAction()
        {
            Action = ActionType.notify;
        }
    }
}
