using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nexmo.Api.Conversations
{
    public class UserList
    {
        [JsonProperty("users")]
        public IList<User> Users { get; set; }
    }
}
