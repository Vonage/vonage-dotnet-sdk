using Newtonsoft.Json;

namespace Nexmo.Api.Accounts
{
    public class Secret
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; } 
    }
}