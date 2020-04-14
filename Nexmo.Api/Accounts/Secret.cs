using Newtonsoft.Json;
using Nexmo.Api.Common;

namespace Nexmo.Api.Accounts
{
    public class Secret
    {
        [JsonProperty("_links")]
        public HALLinks Links { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; } 
    }
}