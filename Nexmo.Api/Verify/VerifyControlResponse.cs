using Newtonsoft.Json;

namespace Nexmo.Api.Verify
{
    public class VerifyControlResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("error_text")]
        public string ErrorText { get; set; }

        [JsonProperty("command")]
        public string Command { get; set; }
    }
}