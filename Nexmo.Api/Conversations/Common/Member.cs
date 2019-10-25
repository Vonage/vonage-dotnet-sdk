using Newtonsoft.Json;

namespace Nexmo.Api.Conversations
{
    public class Member
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("conversation_id")]
        public string ConversationId { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("timestamp")]
        public MemberTimestamp Timestamp { get; set; }

        [JsonProperty("channel")]
        public Channel Channel { get; set; }

        [JsonProperty("initiator")]
        public Initiator Initiator { get; set; }

        [JsonProperty("media")]
        public Media Media { get; set; }

        [JsonProperty("_links")]
        public SingleRecordLink Links { get; set; }

    }
}
