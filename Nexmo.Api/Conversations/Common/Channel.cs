using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nexmo.Api.Conversations
{
    public class Channel
    {
        public enum MemberChannelType
        {
            app = 1
        }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public MemberChannelType Type { get; set; }
    }
}
