using Newtonsoft.Json;

namespace Nexmo.Api.NumberInsights
{
    public class CallerId
    {
        [JsonProperty("caller_type")]
        public string CallerType { get; set; }

        [JsonProperty("caller_name")]
        public string CallerName { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }
    }
}