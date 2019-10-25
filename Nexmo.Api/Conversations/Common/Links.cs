using Newtonsoft.Json;

namespace Nexmo.Api.Conversations
{
    public class Links
    {
        [JsonProperty("first")]
        public Link First { get; set; }

        [JsonProperty("self")]
        public Link Self { get; set; }

        [JsonProperty("next")]
        public Link Next { get; set; }

        [JsonProperty("prev")]
        public Link Previous { get; set; }
    }
}
