using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Nexmo.Api.Conversations
{
    public class CreateTextEventRequest : CreateEventRequestBase
    {
        [JsonProperty("type")]
        public string Type { get; private set; } = "text";

        [JsonProperty("body")]
        public CreateConversationTextEventBody Body { get; set; }

        public class CreateConversationTextEventBody
        {
            [JsonProperty("text")]
            public string Text { get; set; }
        }
    }
}
