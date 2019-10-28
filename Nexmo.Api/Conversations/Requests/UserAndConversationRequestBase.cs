using Newtonsoft.Json;

namespace Nexmo.Api.Conversations
{
    public abstract class UserAndConversationRequestBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }        

        [JsonProperty("properties")]
        public Properties Properties { get; set; }
    }
}
