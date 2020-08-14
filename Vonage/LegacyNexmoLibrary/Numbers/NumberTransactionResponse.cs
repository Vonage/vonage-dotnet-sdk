using Newtonsoft.Json;

namespace Nexmo.Api.Numbers
{
    [System.Obsolete("The Nexmo.Api.Numbers.NumberTransactionResponse class is obsolete. " +
        "References to it should be switched to the new Vonage.Numbers.NumberTransactionResponse class.")]
    public class NumberTransactionResponse
    {
        /// <summary>
        /// The status code of the response. 200 indicates a successful request.
        /// </summary>
        [JsonProperty("error-code")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// The status code description
        /// </summary>
        [JsonProperty("error-code-label")]
        public string ErrorCodeLabel { get; set; }
    }
}