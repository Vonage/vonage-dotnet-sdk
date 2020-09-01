using Newtonsoft.Json;

namespace Nexmo.Api.ShortCodes
{
    [System.Obsolete("The Nexmo.Api.ShortCodes.OptInQueryRequest class is obsolete. " +
        "References to it should be switched to the new Vonage.ShortCodes.OptInQueryRequest class.")]
    public class OptInQueryRequest
    {
        [JsonProperty("page-size")]
        public string PageSize { get; set; }

        [JsonProperty("page")]
        public string Page { get; set; }
        
    }
}