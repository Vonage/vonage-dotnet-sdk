using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Request
{
    public class UpdateMemberRequest
    {
        public enum MemberState 
        { 
            join=1
        }

        [JsonProperty("state")]
        public MemberState State { get; set; }

        [JsonProperty("channel")]
        public Channel Channel { get; set; }
    }
}
