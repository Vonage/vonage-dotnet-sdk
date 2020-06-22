using Newtonsoft.Json;

namespace Nexmo.Api.NumberInsights
{
    public class AdvancedNumberInsightRequest : StandardNumberInsightRequest
    {
        /// <summary>
        /// This parameter is deprecated as we are no longer able to retrieve reliable IP data globally from carriers.
        /// </summary>
        [JsonProperty("ip")]
        public string Ip { get; set; }
    }
}