using Newtonsoft.Json;
using System;

namespace Nexmo.Api.Voice.EventWebhooks
{
    public class Transfer : Event
    {
        [JsonProperty("conversation_uuid_from")]
        public string ConversationUuidFrom { get; set; }
        
        [JsonProperty("conversation_uuid_to")]
        public string ConversationUuidTo { get; set; }
        
    }
}
