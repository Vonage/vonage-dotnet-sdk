using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nexmo.Api.NumberInsights
{
    [System.Obsolete("The Nexmo.Api.NumberInsights.Roaming class is obsolete. " +
        "References to it should be switched to the new Vonage.NumberInsights.Roaming class.")]
    public class Roaming
    {
        /// <summary>
        /// Is number outside its home carrier network.
        /// </summary>
        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RoamingStatus Status { get; set; }

        /// <summary>
        /// If number is roaming, this is the code of the country number is roaming in.
        /// </summary>
        [JsonProperty("roaming_country_code")]
        public string RoamingCountryCode { get; set; }

        /// <summary>
        /// If number is roaming, this is the id of the carrier network number is roaming in.
        /// </summary>
        [JsonProperty("roaming_network_code")] 
        public string RoamingNetworkCode { get; set; }

        /// <summary>
        /// If number is roaming, this is the name of the carrier network number is roaming in.
        /// </summary>
        [JsonProperty("roaming_network_name")]
        public string RoamingNetworkName { get; set; }
    }
}