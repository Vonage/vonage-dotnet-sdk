using Newtonsoft.Json;

namespace Vonage.Accounts
{
    public class SecretList
    {
        [JsonProperty("secrets")]
        public Secret[] Secrets { get; set; }
    }
}