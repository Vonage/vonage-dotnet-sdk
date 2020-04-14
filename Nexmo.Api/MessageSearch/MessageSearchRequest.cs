using Newtonsoft.Json;

namespace Nexmo.Api.MessageSearch
{
    public class MessageSearchRequest
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}