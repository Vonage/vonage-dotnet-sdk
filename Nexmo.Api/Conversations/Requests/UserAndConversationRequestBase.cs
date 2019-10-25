using Newtonsoft.Json;

namespace Nexmo.Api.Conversations
{
    public abstract class UserAndConversationRequestBase<T> where T : class
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("properties")]
        public Properties<T> Properties { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }        
    }
}
