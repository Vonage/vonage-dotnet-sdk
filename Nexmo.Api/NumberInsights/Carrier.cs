using Newtonsoft.Json;

namespace Nexmo.Api.NumberInsights
{
    public class Carrier
    {
        [JsonProperty("network_code")]
        public string NetworkCode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("network_type")]
        public string NetworkType { get; set; }
    }
}