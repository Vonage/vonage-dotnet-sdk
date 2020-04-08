using Newtonsoft.Json;
using Nexmo.Api.Applications.Capabilities;

namespace Nexmo.Api.Applications
{
    public class CreateApplicationRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("capabilities")]
        public Capability[] Capabilities { get; set; }
        
        [JsonProperty("keys")]
        public Keys Keys { get; set; }

    }
}