using Newtonsoft.Json;

namespace Nexmo.Api.Numbers
{
    [System.Obsolete("The Nexmo.Api.Numbers.NumberTransactionRequest class is obsolete. " +
        "References to it should be switched to the new Vonage.Numbers.NumberTransactionRequest class.")]
    public class NumberTransactionRequest
    {
        /// <summary>
        /// The two character country code in ISO 3166-1 alpha-2 format
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        /// An available inbound virtual number.
        /// </summary>
        [JsonProperty("msisdn")]
        public string Msisdn { get; set; }
        /// <summary>
        /// If you’d like to perform an action on a subaccount, provide the 
        /// api_key of that account here. If you’d like to perform an action on your own account, 
        /// you do not need to provide this field.
        /// </summary>
        [JsonProperty("target_api_key")]
        public string TargetApiKey { get; set; }
    }
}