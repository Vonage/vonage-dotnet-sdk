using Newtonsoft.Json;
using Nexmo.Api.Common;
namespace Nexmo.Api.ShortCodes
{
    public class AlertResponse
    {
        [JsonProperty("message-count")]
        public string MessageCount { get; set; }

        
        public Message[] Messages { get; set; }
    }
}