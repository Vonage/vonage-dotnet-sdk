using Newtonsoft.Json;

namespace Vonage.Verify
{
    public class VerifyResponse : VerifyResponseBase
    {
        /// <summary>
        ///     The unique ID of the Verify request. This may be blank in an error situation.
        /// </summary>
        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        /// <summary>
        ///     The Network ID of the blocking network, if relevant to the error.
        /// </summary>
        [JsonProperty("network")]
        public string Network { get; set; }
    }
}