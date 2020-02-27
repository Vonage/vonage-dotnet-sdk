using Newtonsoft.Json;

namespace Nexmo.Api.Accounts
{
    public class SecretsRequestResult
    {
        [JsonProperty("_links")]
        public string Links { get; set; }

        [JsonProperty("_embedded")]
        public SecretList Embedded { get; set; }
    }
}