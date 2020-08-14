using Newtonsoft.Json;
using Vonage.Common;

namespace Vonage.Accounts
{
    public class Secret
    {
        /// <summary>
        /// the reference link for the secret
        /// </summary>
        [JsonProperty("_links")]
        public HALLinks Links { get; set; }

        /// <summary>
        /// the id of the secret
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// the creation time of the secret
        /// </summary>
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; } 
    }
}