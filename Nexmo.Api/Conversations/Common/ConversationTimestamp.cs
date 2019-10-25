using Newtonsoft.Json;

namespace Nexmo.Api.Conversations
{
    public class ConversationTimestamp
    {
        [JsonProperty("created")]
        public string Created { get; set; }
    }
}
