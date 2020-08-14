using Newtonsoft.Json;

namespace Nexmo.Api.ShortCodes
{
    [System.Obsolete("The Nexmo.Api.ShortCodes.OptInRecord class is obsolete. " +
        "References to it should be switched to the new Vonage.ShortCodes.OptInRecord class.")]
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