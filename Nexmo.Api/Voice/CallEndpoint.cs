using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Voice
{
    public class CallEndpoint
    {
        /// <summary>
        /// One of the following: phone, websocket, sip
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// The phone number to connect to in E.164 format.
        /// </summary>
        [JsonProperty("number")]
        public string Number { get; set; }

        /// <summary>
        /// Set the digits that are sent to the user as soon as the Call is answered. The * and # digits are respected. You create pauses using p. Each pause is 500ms.
        /// </summary>
        [JsonProperty("dtmfAnswer")]
        public string DtmfAnswer { get; set; }        

        /// <summary>
        /// The URI to the websocket you are streaming to.
        /// OR
        /// The SIP URI to the endpoint you are connecting to in the format sip:rebekka@sip.example.com.
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; set; }

        // websocket:

        /// <summary>
        /// The internet media type for the audio you are streaming.Possible values are: audio/l16; rate=16000
        /// </summary>
        [JsonProperty("content-type")]
        public string ContentType { get; set; }

        /// <summary>
        /// A JSON object containing any metadata you want.
        /// </summary>
        [JsonProperty("headers")]
        public object Headers { get; set; }
    }
}
