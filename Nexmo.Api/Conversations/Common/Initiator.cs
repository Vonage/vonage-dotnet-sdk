using Newtonsoft.Json;

namespace Nexmo.Api.Conversations
{
    public class Initiator
    {
        [JsonProperty("invited")]
        public InitiatorObject Invited { get; set; }

        [JsonProperty("joined")]
        public InitiatorObject Joined { get; set; }

        public class InitiatorObject
        {
            [JsonProperty("is_system")]
            public bool IsSystem { get; set; }
        }
    }
}
