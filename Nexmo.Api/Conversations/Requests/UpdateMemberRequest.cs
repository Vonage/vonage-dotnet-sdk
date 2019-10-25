using Newtonsoft.Json;

namespace Nexmo.Api.Conversations
{
    public class UpdateMemberRequest
    {
        public enum MemberState 
        { 
            join=1
        }

        [JsonProperty("state")]
        public MemberState State { get; set; }

        [JsonProperty("channel")]
        public Channel Channel { get; set; }
    }
}
