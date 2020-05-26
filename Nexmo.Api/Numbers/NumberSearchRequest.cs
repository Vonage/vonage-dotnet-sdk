using Newtonsoft.Json;

namespace Nexmo.Api.Numbers
{
    public class NumberSearchRequest
    {
        /// <summary>
        /// The two character country code in ISO 3166-1 alpha-2 format
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        /// Set this parameter to filter the type of number, such as mobile or landline
        /// Must be one of: landline, mobile-lvn or landline-toll-free
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// The number pattern you want to search for. Use in conjunction with search_pattern.
        /// </summary>
        [JsonProperty("pattern")]
        public string Pattern { get; set; }

        /// <summary>
        /// The strategy you want to use for matching
        /// </summary>
        [JsonProperty("search_pattern")]
        public SearchPattern? SearchPattern { get; set; }

        /// <summary>
        /// Available features are SMS, VOICE and MMS. To look for numbers that support multiple features, use a comma-separated value: SMS,MMS,VOICE.
        /// Must be one of: SMS, VOICE, SMS,VOICE, MMS, SMS,MMS, VOICE,MMS or SMS,MMS,VOICE
        /// </summary>
        [JsonProperty("features")]
        public string Features { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        [JsonProperty("size", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public int? Size { get; set; }

        /// <summary>
        /// Page index
        /// </summary>
        [JsonProperty("index", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)] 
        public int? Index { get; set; }

        /// <summary>
        /// Set this optional field to true to restrict your results to numbers associated with an Application (any Application). 
        /// Set to false to find all numbers not associated with any Application. Omit the field to avoid filtering on whether or not the number is assigned to an Application.
        /// </summary>
        [JsonProperty("has_application", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasApplication { get; set; }
    }
}