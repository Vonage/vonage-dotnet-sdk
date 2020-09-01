using Newtonsoft.Json;

namespace Nexmo.Api.Applications
{
    [System.Obsolete("The Nexmo.Api.Applications.Keys class is obsolete. " +
        "References to it should be switched to the new Vonage.Applications.Keys class.")]
    public class Keys
    {
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        [JsonProperty("private_key")]
        public string PrivateKey { get; set; }
    }
}