using Newtonsoft.Json;

namespace Nexmo.Api.Conversations
{
    public class MemberTimestamp
    {
        [JsonProperty("invited")]
        public string Invited { get; set; }

        [JsonProperty("joined")]
        public string Joined { get; set; }

        [JsonProperty("left")]
        public string Left { get; set; }
    }
}
