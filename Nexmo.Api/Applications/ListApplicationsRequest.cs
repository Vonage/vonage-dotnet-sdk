using Newtonsoft.Json;

namespace Nexmo.Api.Applications
{
    public class ListApplicationsRequest
    {
        [JsonProperty("page_size")]
        public int PageSize { get; set; }
        
        [JsonProperty("page")]
        public int Page { get; set; }
    }
}