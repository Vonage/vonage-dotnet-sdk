using Newtonsoft.Json;

namespace Nexmo.Api.Numbers
{
    public class NumberTransactionRequest
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("msisdn")]
        public string Msisdn { get; set; }

        [JsonProperty("target_api_key")]
        public string TargetApiKey { get; set; }
    }
}