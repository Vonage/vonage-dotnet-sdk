using Newtonsoft.Json;

namespace Nexmo.Api.Conversations
{
    public class Conversation
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("timestamp")]
        public ConversationTimestamp Timestamp { get; set; }

        [JsonProperty("properties")]
        public Properties Properties { get; set; }

        [JsonProperty("_links")]
        public SingleRecordLink Links { get; set; }
    }
}
