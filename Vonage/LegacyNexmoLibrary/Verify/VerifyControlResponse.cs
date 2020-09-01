using Newtonsoft.Json;

namespace Nexmo.Api.Verify
{
    [System.Obsolete("The Nexmo.Api.Verify.VerifyControlResponse class is obsolete. " +
        "References to it should be switched to the new Vonage.Verify.VerifyControlResponse class.")]
    public class VerifyControlResponse : VerifyResponseBase
    {
        [JsonProperty("command")]
        public string Command { get; set; }
    }
}