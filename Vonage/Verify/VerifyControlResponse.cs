using Newtonsoft.Json;

namespace Vonage.Verify
{
    public class VerifyControlResponse : VerifyResponseBase
    {
        [JsonProperty("command")]
        public string Command { get; set; }
    }
}