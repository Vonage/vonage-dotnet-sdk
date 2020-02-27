using Newtonsoft.Json;

namespace Nexmo.Api.Accounts
{
    public class SecretList
    {
        [JsonProperty("secrets")]
        public Secret Secrets { get; set; }
    }
}