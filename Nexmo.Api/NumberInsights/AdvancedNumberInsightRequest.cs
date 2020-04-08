using Newtonsoft.Json;

namespace Nexmo.Api.NumberInsights
{
    public class AdvancedNumberInsightRequest : StandardNumberInsightRequest
    {
        [JsonProperty("ip")]
        public string Ip { get; set; }
    }
}