using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nexmo.Api.Conversations
{
    public class MemberList
    {
        [JsonProperty("members")]
        public IList<Member> Members { get; set; }
    }
}
