using Newtonsoft.Json;

namespace Nexmo.Api.Accounts
{
    [System.Obsolete("The Nexmo.Api.Accounts.SecretList class is obsolete. " +
        "References to it should be switched to the new Vonage.Accounts.SecretList class.")]
    public class SecretList
    {
        [JsonProperty("secrets")]
        public Secret[] Secrets { get; set; }
    }
}