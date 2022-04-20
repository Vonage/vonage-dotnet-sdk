using Newtonsoft.Json;

namespace Vonage.NumberInsights
{
    public class RealTimeData
    {
        /// <summary>
        /// Whether the end-user's phone number is active within an operator's network.
        /// </summary>
        [JsonProperty("active_status")]
        public bool ActiveStatus { get; set; }

        /// <summary>
        /// Whether the end-user's handset is turned on or off.
        /// </summary>
        [JsonProperty("handset_status")]
        public string HandsetStatus { get; set; }
    }
}