using Newtonsoft.Json;

namespace Nexmo.Api.Verify
{
    [System.Obsolete("The Nexmo.Api.Verify.VerifyEvent class is obsolete. " +
        "References to it should be switched to the new Vonage.Verify.VerifyEvent class.")]
    public class VerifyEvent
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}