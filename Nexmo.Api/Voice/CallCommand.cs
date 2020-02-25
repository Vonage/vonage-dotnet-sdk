using Newtonsoft.Json;
using Nexmo.Api.Voice.Nccos;
using Nexmo.Api.Voice.Nccos.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Voice
{
    [JsonConverter(typeof(CallCommandConverter))]
    public class CallCommand
    {
        /// <summary>
        /// The single or mixed collection of endpoint types you connected to. Possible values.
        /// </summary>
        [JsonProperty(Required = Required.Always, PropertyName = "to")]
        public Endpoint[] To { get; set; }
        /// <summary>
        /// The endpoint you are calling from. Possible value are the same as to.
        /// </summary>
        [JsonProperty(Required = Required.Always, PropertyName = "from")]
        public Endpoint From { get; set; }

        /// <summary>
        /// This will convert to ncco as per the CallCommandConverter - it is preferable to use this over the JArray Ncco
        /// </summary>
        public Ncco Ncco { get; set; }

        /// <summary>
        /// The webhook endpoint where you provide the Nexmo Call Control Object that governs this call. As soon as your user answers a call, Platform requests this NCCO from answer_url. Use answer_method to manage the HTTP method.
        /// </summary>
        [JsonProperty("answer_url")]
        public string[] AnswerUrl { get; set; }

        /// <summary>
        /// Optional. The HTTP method used to send event information to answer_url. The default value is GET.
        /// </summary>
        [JsonProperty("answer_method")]
        public string AnswerMethod { get; set; }

        /// <summary>
        /// Optional. Platform sends event information asynchronously to this endpoint when status changes. For more information about the values sent, see callback.
        /// </summary>
        [JsonProperty("event_url")]
        public string[] EventUrl { get; set; }

        /// <summary>
        /// Optional. The HTTP method used to send event information to event_url. The default value is POST.
        /// </summary>
        [JsonProperty("event_method")]
        public string EventMethod { get; set; }

        /// <summary>
        /// Optional. Configure the behavior when Nexmo detects that a destination is an answerphone.
        /// </summary>
        [JsonProperty("MachineDetection")]
        public string MachineDetection { get; set; }

        /// <summary>
        /// Optional. Set the number of seconds that elapse before Nexmo hangs up after the call state changes to in_progress. The default value is 7200, two hours. This is also the maximum value.
        /// </summary>
        [JsonProperty("length_timer")]
        public decimal LengthTimer { get; set; }

        /// <summary>
        /// Optional. Set the number of seconds that elapse before Nexmo hangs up after the call state changes to 'ringing'. The default value is 60, the maximum value is 120.
        /// </summary>
        [JsonProperty("ringing_timer")]
        public decimal RingingTimer { get; set; }
    }
}
