using Newtonsoft.Json;

namespace Nexmo.Api.ShortCodes
{
    public class OptInQueryRequest
    {
        [JsonProperty("page-size")]
        public string PageSize { get; set; }

        [JsonProperty("page")]
        public string Page { get; set; }
        
    }
}