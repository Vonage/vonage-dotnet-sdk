using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Voice.EventWebhooks
{
    public class DtmfResult
    {
        [JsonProperty("digits")]
        public string Digits { get; set; }

        [JsonProperty("timed_out")]
        public bool TimedOut { get; set; }

    }
}
