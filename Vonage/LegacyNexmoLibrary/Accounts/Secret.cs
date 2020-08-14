using Newtonsoft.Json;
using Nexmo.Api.Common;

namespace Nexmo.Api.Accounts
{
    [System.Obsolete("The Nexmo.Api.Accounts.Secret class is obsolete. " +
        "References to it should be switched to the new Vonage.Accounts.Secret class.")]
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