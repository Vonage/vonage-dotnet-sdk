using Newtonsoft.Json;

namespace Nexmo.Api.Conversations
{
    public class CursorBasedListResponse <T> where T : class
    {
        [JsonProperty("page_size")]
        public int PageSize { get; set; }

        [JsonProperty("_links")]
        public Links Links { get; set; }

        [JsonProperty("_embedded")]
        public T Embedded { get; set; }
    }
}
