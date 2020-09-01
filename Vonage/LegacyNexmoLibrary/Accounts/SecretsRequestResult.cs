using Newtonsoft.Json;
using Nexmo.Api.Common;
namespace Nexmo.Api.Accounts
{
    [System.Obsolete("The Nexmo.Api.Accounts.SecretsRequestResult class is obsolete. " +
        "References to it should be switched to the new Vonage.Accounts.SecretsRequestResult class.")]
    public class SecretsRequestResult
    {
        /// <summary>
        /// reference links for the secrets
        /// </summary>
        [JsonProperty("_links")]
        public HALLinks Links { get; set; }

        /// <summary>
        /// the secrets
        /// </summary>
        [JsonProperty("_embedded")]
        public SecretList Embedded { get; set; }
    }
}