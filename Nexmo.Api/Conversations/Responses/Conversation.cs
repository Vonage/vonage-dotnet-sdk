using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ConversationProperties Properties { get; set; }

        public class ConversationTimestamp
        {
            [JsonProperty("created")]
            public string Created { get; set; }
        }

        public class ConversationProperties
        {
            [JsonProperty("custom_data")]
            public string CustomData { get; set; }
        }
    }
}
