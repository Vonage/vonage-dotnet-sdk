using Newtonsoft.Json;

namespace Nexmo.Api.Verify
{
    [System.Obsolete("The Nexmo.Api.Verify.VerifyResponse class is obsolete. " +
        "References to it should be switched to the new Vonage.Verify.VerifyResponse class.")]
    public class VerifyResponse : VerifyResponseBase
    {
        [JsonProperty("request_id")]
        public string RequestId { get; set; }
    }
}