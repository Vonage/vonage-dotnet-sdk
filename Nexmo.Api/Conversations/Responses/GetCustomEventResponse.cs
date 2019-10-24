using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Conversations
{
    public class GetCustomEventResponse : EventBase
    {
        [JsonProperty("body")]
        public Dictionary<string,string> Body { get; set; }
    }
}
