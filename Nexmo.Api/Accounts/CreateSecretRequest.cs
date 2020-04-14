using Newtonsoft.Json;

namespace Nexmo.Api.Accounts
{
    public class CreateSecretRequest
    {
        [JsonProperty("secret")]
        public string Secret { get; set; }
    }
}