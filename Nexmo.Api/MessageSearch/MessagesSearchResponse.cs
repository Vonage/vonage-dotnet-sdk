using Newtonsoft.Json;
using Nexmo.Api.Common;
namespace Nexmo.Api.MessageSearch
{
    public class MessagesSearchResponse
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("items")]
        public Message[] Items { get; set; }
    }
}