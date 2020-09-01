using System.ComponentModel;
using System.Net.Http;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vonage.Common
{
    public class Webhook
    {
        [JsonProperty("http_method")]
        public string Method { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
        
        public enum Type
        {            
            answer_url=1,
            event_url =2,
            inbound_url =3,
            status_url =4,
            fallback_answer_url=5,
            Unknown=6
        }
        
    }
}