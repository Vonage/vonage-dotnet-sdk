using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nexmo.Api.Conversations
{
    public class Event
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }        

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("body")]
        public JObject Body { get; set; }

        [JsonProperty("conversation_id")]
        public string ConversationId { get; set; }

        public T GetBodyAsType<T>()
        {
            return JsonConvert.DeserializeObject<T>(Body.ToString());
        }
    }
}
