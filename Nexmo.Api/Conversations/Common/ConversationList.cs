using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nexmo.Api.Conversations
{
    public class ConversationList
    {
        [JsonProperty("conversations")]
        public IList<Conversation> Conversations { get; set; }
    }
}
