using Newtonsoft.Json;

namespace Nexmo.Api.Conversations
{
    public class Link
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }
}
