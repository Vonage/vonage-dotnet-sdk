using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Conversations
{
    public abstract class UserAndConversationRequestBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("properties")]
        public ConversationProperties Properties { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        public class ConversationProperties
        {
            [JsonProperty("custom_data")]
            public object CustomData { get; set; }
        }
    }
}
