using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nexmo.Api.Conversations
{
    public class UpdateMemberRequest
    {
        public enum MemberState 
        { 
            join=1
        }

        [JsonProperty("state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public MemberState State { get; set; }

        [JsonProperty("channel")]
        public Channel Channel { get; set; }
    }
}
