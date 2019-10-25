using Newtonsoft.Json;

namespace Nexmo.Api.Conversations
{
    public class CreateMemberWithIdRequest : CreateMemberRequestBase
    {
        [JsonProperty("user_id")]
        public string Id { get; set; }
    }
}
