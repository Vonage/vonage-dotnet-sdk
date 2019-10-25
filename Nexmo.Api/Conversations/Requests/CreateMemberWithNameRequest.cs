using Newtonsoft.Json;

namespace Nexmo.Api.Conversations
{
    public class CreateMemberWithNameRequest :CreateMemberRequestBase
    {
        [JsonProperty("user_name")]
        public string Name { get; set; }
    }
}
