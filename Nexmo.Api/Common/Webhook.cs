using System.ComponentModel;
using System.Net.Http;
using Newtonsoft.Json;

namespace Nexmo.Api.Common
{
    public class Webhook
    {
        [JsonProperty("http_method")]
        public HttpMethod Method { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
        
        public enum Type
        {
            [Description("answer_url")]
            Answer=1,
            [Description("event_url")]
            Event=2,
            [Description("inbound_url")]
            Inbound=3,
            [Description("status_url")]
            Status=4,
            [Description("unknown")]
            Unknown=5
        }
        
    }
}