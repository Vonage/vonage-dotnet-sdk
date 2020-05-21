using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.NumberInsights
{
    public class NumberInsightResponseBase
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("request_id")]
        public string RequestId { get; set; }
    }
}
