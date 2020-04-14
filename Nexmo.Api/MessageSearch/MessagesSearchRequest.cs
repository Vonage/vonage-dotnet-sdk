using Newtonsoft.Json;

namespace Nexmo.Api.MessageSearch
{
    public class MessagesSearchRequest : MessageSearchRequest
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }
    }
}