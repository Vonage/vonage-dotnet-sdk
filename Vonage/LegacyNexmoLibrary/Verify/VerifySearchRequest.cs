using Newtonsoft.Json;

namespace Nexmo.Api.Verify
{
    [System.Obsolete("The Nexmo.Api.Verify.VerifySearchRequest class is obsolete. " +
        "References to it should be switched to the new Vonage.Verify.VerifySearchRequest class.")]
    public class VerifySearchRequest
    {
        /// <summary>
        /// The request_id you received in the Verify Request Response.
        /// </summary>
        [JsonProperty("request_id")]
        public string RequestId { get; set; }
    }    
}