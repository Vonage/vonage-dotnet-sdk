using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Conversations
{
    public class CreateCustomEventRequest : CreateEventRequestBase
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("body")]
        public object Body { get; set; }        
    }
}
