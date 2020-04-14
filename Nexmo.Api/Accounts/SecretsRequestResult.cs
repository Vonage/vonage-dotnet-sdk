using Newtonsoft.Json;
using Nexmo.Api.Common;
namespace Nexmo.Api.Accounts
{
    public class SecretsRequestResult
    {
        [JsonProperty("_links")]
        public HALLinks Links { get; set; }

        [JsonProperty("_embedded")]
        public SecretList Embedded { get; set; }
    }
}