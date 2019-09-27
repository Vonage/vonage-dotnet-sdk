using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nexmo.Api.Voice.EventWebhooks
{
    public class HumanMachine : CallStatusEvent
    {
        public enum status
        {
            human,
            machine
        }

        [JsonProperty("call_uuid")]
        public override string Uuid { get; set; }

    }
}
