using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.NumberInsights
{
    public class AdvancedInsightsAsyncResponse : NumberInsightResponseBase
    {
        /// <summary>
        /// The number in your request
        /// </summary>
        [JsonProperty("number")]
        public string Number { get; set; }

        /// <summary>
        /// Your account balance in EUR after this request. Not returned with Number Insight Advanced Async API.
        /// </summary>
        [JsonProperty("remaining_balance")]
        public string RemainingBalance { get; set; }

        /// <summary>
        /// If there is an internal lookup error, the refund_price will reflect the lookup price. 
        /// If cnam is requested for a non-US number the refund_price will reflect the cnam price.
        /// If both of these conditions occur, refund_price is the sum of the lookup price and cnam price.
        /// </summary>
        [JsonProperty("request_price")]
        public string RequestPrice { get; set; }
    }
}
