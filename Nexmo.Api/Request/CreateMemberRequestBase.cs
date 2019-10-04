using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Request
{
    public abstract class CreateMemberRequestBase
    {
        [JsonProperty("channel")]
        public Channel Channel { get; set; }

        [JsonProperty("conversation_id")]
        public string ConversationId { get; set; }
    }
}
