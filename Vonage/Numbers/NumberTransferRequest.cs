using Newtonsoft.Json;

namespace Vonage.Numbers
{
    public class NumberTransferRequest
    {
        /// <summary>
        /// The two character country code in ISO 3166-1 alpha-2 format
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }
        
        /// <summary>
        /// An owned virtual number to be transferred.
        /// </summary>
        [JsonProperty("number")]
        public string Number { get; set; }

        /// <summary>
        /// The account that currently holds the number.
        /// </summary>
        [JsonProperty("from")]
        public string From { get; set; }
        
        /// <summary>
        /// The subaccount to transfer the number to.
        /// </summary>
        [JsonProperty("to")]
        public string To { get; set; }
    }
}