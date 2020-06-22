using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nexmo.Api.NumberInsights
{
    public class AdvancedInsightsResponse : StandardInsightResponse
    {
        /// <summary>
        /// Shows if all information about a phone number has been returned.
        /// </summary>
        [JsonProperty("lookup_outcome")]        
        public int LookupOutcome { get; set; }

        /// <summary>
        /// Shows if all information about a phone number has been returned.
        /// </summary>
        [JsonProperty("lookup_outcome_message")]        
        public string LookupOutcomeMessage { get; set; }

        /// <summary>
        /// Does number exist. unknown means the number could not be validated. 
        /// valid means the number is valid. not_valid means the number is not valid. 
        /// inferred_not_valid means that the number could not be determined as valid or invalid 
        /// via an external system and the best guess is that the number is invalid. 
        /// This is applicable to mobile numbers only.
        /// </summary>

        [JsonProperty("valid_number")]
        [JsonConverter(typeof(StringEnumConverter))]
        public NumberValidity ValidNumber { get; set; }

        /// <summary>
        /// Can you call number now. This is applicable to mobile numbers only.
        /// </summary>
        [JsonProperty("reachable")]
        [JsonConverter(typeof(StringEnumConverter))]
        public NumberReachability Reachable { get; set; }
    }
}