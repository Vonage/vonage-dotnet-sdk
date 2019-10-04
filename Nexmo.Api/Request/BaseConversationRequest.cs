using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Request
{
    public abstract class BaseConversationRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("properties")]
        public ConversationProperties Properties { get; set; }
        
        public class ConversationProperties
        {
            [JsonProperty("custom_data")]
            public string CustomData { get; set; }
        }
    }
}
