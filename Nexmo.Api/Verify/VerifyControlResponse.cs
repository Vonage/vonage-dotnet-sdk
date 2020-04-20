using Newtonsoft.Json;

namespace Nexmo.Api.Verify
{
    public class VerifyControlResponse : VerifyResponseBase
    {
        [JsonProperty("command")]
        public string Command { get; set; }
    }
}