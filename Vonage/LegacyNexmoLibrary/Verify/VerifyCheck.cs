using Newtonsoft.Json;

namespace Nexmo.Api.Verify
{
    [System.Obsolete("The Nexmo.Api.Verify.VerifyCheck class is obsolete. " +
        "References to it should be switched to the new Vonage.Verify.VerifyCheck class.")]
    public class VerifyCheck
    {
        /// <summary>
        /// The date and time this check was received (in the format YYYY-MM-DD HH:MM:SS)
        /// </summary>
        [JsonProperty("date_received")]
        public string DateReceived { get; set; }

        /// <summary>
        /// The code supplied with this check request
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// One of: VALID or INVALID
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// The IP address, if available (this field is no longer used).
        /// </summary>
        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }
    }
}