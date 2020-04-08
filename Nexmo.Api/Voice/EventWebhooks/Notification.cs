using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nexmo.Api.Voice.EventWebhooks
{
    public class Notification<T> : EventBase
    {
        [JsonProperty("payload")]
        public T Payload { get; set; }

        [JsonProperty("conversation_uuid")]
        public string ConversationUuid { get; set; }
    }
}
