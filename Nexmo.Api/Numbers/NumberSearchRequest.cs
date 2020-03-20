using Newtonsoft.Json;

namespace Nexmo.Api.Numbers
{
    public class NumberSearchRequest
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("pattern")]
        public string Pattern { get; set; }

        [JsonProperty("search_pattern")]
        public int? SearchPattern { get; set; }

        [JsonProperty("features")]
        public string Features { get; set; }
        
        [JsonProperty("size", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public int? Size { get; set; }
        
        [JsonProperty("index", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)] 
        public int? Index { get; set; }

        [JsonProperty("has_application", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasApplication { get; set; }
    }
}