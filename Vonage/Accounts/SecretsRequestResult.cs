using Newtonsoft.Json;
using Vonage.Common;
namespace Vonage.Accounts
{
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