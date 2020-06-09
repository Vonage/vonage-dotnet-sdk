using Newtonsoft.Json;

namespace Nexmo.Api.Verify
{
    public class VerifyCheckResponse : VerifyResponseBase
    {
        /// <summary>
        /// The request_id that you received in the response to the Verify request and used in the Verify check request.
        /// </summary>
        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        /// <summary>
        /// The ID of the verification event, such as an SMS or TTS call.
        /// </summary>
        [JsonProperty("event_id")]
        public string EventId { get; set; }

        /// <summary>
        /// The cost incurred for this request.
        /// </summary>
        [JsonProperty("price")]
        public string Price { get; set; }

        /// <summary>
        /// The currency code.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// This field may not be present, depending on your pricing model. 
        /// The value indicates the cost (in EUR) of the calls made and messages sent for the verification process. 
        /// This value may be updated during and shortly after the request completes because user input events can overlap with message/call events. 
        /// When this field is present, the total cost of the verification is the sum of this field and the price field.
        /// </summary>
        [JsonProperty("estimated_price_messages_sent")]
        public string EstimatedPriceMessagesSent { get; set; }        
    }
}