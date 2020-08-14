using Newtonsoft.Json;

namespace Nexmo.Api.Pricing
{
    [System.Obsolete("The Nexmo.Api.Pricing.Network class is obsolete. " +
        "References to it should be switched to the new Vonage.Pricing.Network class.")]
    public class Network
    {
        /// <summary>
        /// The type of network: mobile or landline.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// The cost to send a message or make a call to this network
        /// </summary>
        [JsonProperty("price")]
        public string Price { get; set; }

        /// <summary>
        /// The currency used for prices for this network.
        /// </summary>
        [JsonProperty("currency")] 
        public string Currency { get; set; }

        /// <summary>
        /// The Mobile Country Code of the operator.
        /// </summary>
        [JsonProperty("mcc")] 
        public string Mcc { get; set; }

        /// <summary>
        /// The Mobile Network Code of the operator.
        /// </summary>
        [JsonProperty("mnc")]
        public string Mnc { get; set; }

        /// <summary>
        /// The Mobile Country Code and Mobile Network Code combined to give a unique reference for the operator.
        /// </summary>
        [JsonProperty("networkCode")]
        public string NetworkCode { get; set; }

        /// <summary>
        /// The company/organisational name of the operator.
        /// </summary>
        [JsonProperty("networkName")]
        public string NetworkName { get; set; }
    }
}