using Newtonsoft.Json;

namespace Nexmo.Api.NumberInsights
{
    public class Roaming
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("roaming_country_code")]
        public string RoamingCountryCode { get; set; }
        
        [JsonProperty("roaming_network_code")] 
        public string RoamingNetworkCode { get; set; }

        [JsonProperty("roaming_network_name")]
        public string RoamingNetworkName { get; set; }
    }
}