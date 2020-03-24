using Newtonsoft.Json;

namespace Nexmo.Api.NumberInsights
{
    public class StandardNumberInsightRequest : BasicNumberInsightRequest
    {
        [JsonProperty("cnam")]
        public bool Cnam { get; set; }
    }
}