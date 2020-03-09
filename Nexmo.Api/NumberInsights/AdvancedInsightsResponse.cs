using Newtonsoft.Json;

namespace Nexmo.Api.NumberInsights
{
    public class AdvancedInsightsResponse : StandardInsightResponse
    {
        [JsonProperty("lookup_outcome")]
        public int LookupOutcome { get; set; }

        [JsonProperty("lookup_outcome_message")]
        public string LookupOutcomeMessage { get; set; }

        [JsonProperty("valid_number")]
        public string ValidNumber { get; set; }

        [JsonProperty("reachable")]
        public string Reachable { get; set; }
    }
}