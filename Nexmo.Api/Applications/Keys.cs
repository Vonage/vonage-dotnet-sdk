using Newtonsoft.Json;

namespace Nexmo.Api.Applications
{
    public class Keys
    {
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }
    }
}