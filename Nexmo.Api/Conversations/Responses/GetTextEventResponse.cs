using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Conversations
{
    public class GetTextEventResponse : EventBase
    {
        [JsonProperty("body")]
        public TextEventBody Body { get; set; }
        public class TextEventBody
        {
            [JsonProperty("text")]
            public string Text { get; set; }
        }
    }
}
