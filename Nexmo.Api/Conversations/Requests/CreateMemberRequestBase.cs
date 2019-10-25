using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nexmo.Api.Conversations
{
    public abstract class CreateMemberRequestBase
    {
        public enum CreateMemberAction
        {
            invite=1,
            join=2
        }

        [JsonProperty("channel")]
        public Channel Channel { get; set; }

        [JsonProperty("action")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CreateMemberAction Action { get; set; }
    }
}
