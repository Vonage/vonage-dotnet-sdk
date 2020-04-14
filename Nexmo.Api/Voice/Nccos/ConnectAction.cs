using Newtonsoft.Json;
using Nexmo.Api.Voice.Nccos.Endpoints;


namespace Nexmo.Api.Voice.Nccos
{
    public class ConnectAction : NccoAction
    {
        [JsonProperty("endpoint")]
        public Endpoint[] Endpoint { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("eventType")]
        public string EventType { get; set; }

        [JsonProperty("timeout")]
        public string Timeout { get; set; }

        [JsonProperty("limit")]
        public string Limit { get; set; }

        [JsonProperty("machineDetection")]
        public string MachineDetection { get; set; }

        [JsonProperty("eventUrl")]
        public string[] EventUrl { get; set; }

        [JsonProperty("eventMethod")]
        public string EventMethod { get; set; }

        public ConnectAction()
        {
            Action = ActionType.connect;
        }

    }
}
