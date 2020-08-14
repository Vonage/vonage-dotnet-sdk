using Newtonsoft.Json;

namespace Nexmo.Api.ShortCodes
{
    [System.Obsolete("The Nexmo.Api.ShortCodes.OptInSearchResponse class is obsolete. " +
        "References to it should be switched to the new Vonage.ShortCodes.OptInSearchResponse class.")]
    public class OptInSearchResponse
    {
        [JsonProperty("opt-in-count")]
        public string OptInCount { get; set; }

        [JsonProperty("opt-in-list")]
        public OptInRecord[] OptInList { get; set; }
    }
}