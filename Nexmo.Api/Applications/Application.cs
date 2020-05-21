using Newtonsoft.Json;
using Nexmo.Api.Applications.Capabilities;

namespace Nexmo.Api.Applications
{
    public class Application
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("capabilities")]
        public ApplicationCapabilities Capabilities { get; set; }

        [JsonProperty("keys")]
        public Keys Keys { get; set; }
    }
}