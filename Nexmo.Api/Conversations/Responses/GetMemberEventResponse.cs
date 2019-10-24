using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Conversations
{
    public class GetMemberEventResponse : EventBase
    {
        [JsonProperty("body")]
        public Member Body { get; set; }
    }
}
