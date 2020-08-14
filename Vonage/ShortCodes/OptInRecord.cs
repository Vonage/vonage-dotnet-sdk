using Newtonsoft.Json;

namespace Vonage.ShortCodes
{
    public class OptInRecord
    {
        [JsonProperty("msisdn")]
        public string Msisdn { get; set; }

        [JsonProperty("opt-in")]
        public string OptIn { get; set; }

        [JsonProperty("opt-in-date")]
        public string OptInDate { get; set; }

        [JsonProperty("opt-out")]
        public string OptOut { get; set; }

        [JsonProperty("opt-out-date")]
        public string OptOutDate { get; set; }
    }
}