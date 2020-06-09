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

        /// <summary>
        /// The unique identifier for this call (Note call_uuid, not uuid as in some other endpoints)
        /// </summary>
        [JsonProperty("call_uuid")]
        public override string Uuid { get; set; }

    }
}
