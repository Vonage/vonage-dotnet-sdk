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

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("remaining_balance")]
        public string RemainingBalance { get; set; }

        [JsonProperty("request_price")]
        public string RequestPrice { get; set; }

        [JsonProperty("error_text")]
        public string ErrorText { get; set; }
    }
}
