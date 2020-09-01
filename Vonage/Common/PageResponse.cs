using Newtonsoft.Json;

namespace Vonage.Common
{
    public class PageResponse<T> where T : class
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("page_size")]
        public int PageSize { get; set; }
        
        [JsonProperty("page_index")]
        public int PageIndex { get; set; }
        
        [JsonProperty("_links")]
        public HALLinks Links { get; set; }
        
        [JsonProperty("_embedded")]
        public T Embedded { get; set; }
    }
}