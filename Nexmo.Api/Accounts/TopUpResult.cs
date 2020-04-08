using Newtonsoft.Json;

namespace Nexmo.Api.Accounts
{
    public class TopUpResult
    {
        [JsonProperty("response")]
        public string Response { get; set; }
    }
}