using Newtonsoft.Json;
using Nexmo.Api.Voice.Nccos.Endpoints;


namespace Nexmo.Api.Voice.Nccos
{
    public class ConnectAction : NccoAction
    {
        /// <summary>
        /// Connect to a single endpoint.
        /// </summary>
        [JsonProperty("endpoint")]
        public Endpoint[] Endpoint { get; set; }

        /// <summary>
        /// A number in E.164 format that identifies the caller.
        /// This must be one of your Nexmo virtual numbers, 
        /// another value will result in the caller ID being unknown.
        /// If the caller is an app user, this option should be omitted.
        /// </summary>
        [JsonProperty("from")]
        public string From { get; set; }

        /// <summary>
        /// Set to synchronous to:
        /// make the connect action synchronous
        /// enable eventUrl to return an NCCO that overrides the current NCCO when a call moves to specific states.
        /// </summary>
        [JsonProperty("eventType")]
        public string EventType { get; set; }

        /// <summary>
        /// If the call is unanswered, set the number in seconds before Nexmo stops ringing endpoint. The default value is 60.
        /// </summary>
        [JsonProperty("timeout")]
        public string Timeout { get; set; }

        /// <summary>
        /// Maximum length of the call in seconds. The default and maximum value is 7200 seconds (2 hours).
        /// </summary>
        [JsonProperty("limit")]
        public string Limit { get; set; }

        /// <summary>
        /// Configure the behavior when Nexmo detects that a destination is an answerphone. Set to either:
        /// continue - Nexmo sends an HTTP request to event_url with the Call event machine
        /// hangup - end the Call
        /// </summary>
        [JsonProperty("machineDetection")]
        public string MachineDetection { get; set; }

        /// <summary>
        /// Set the webhook endpoint that Nexmo calls asynchronously on each of the possible Call States. 
        /// If eventType is set to synchronous the eventUrl can return an NCCO that overrides the current 
        /// NCCO when a timeout occurs.
        /// </summary>
        [JsonProperty("eventUrl")]
        public string[] EventUrl { get; set; }

        /// <summary>
        /// The HTTP method Nexmo uses to make the request to eventUrl. The default value is POST.
        /// </summary>
        [JsonProperty("eventMethod")]
        public string EventMethod { get; set; }

        /// <summary>
        /// A URL value that points to a ringbackTone to be played back on repeat to the caller, 
        /// so they don't hear silence. The ringbackTone will automatically stop playing when the call is 
        /// fully connected. It's not recommended to use this parameter when connecting to a phone endpoint, 
        /// as the carrier will supply their own ringbackTone. 
        /// Example: "ringbackTone": "http://example.com/ringbackTone.wav".
        /// </summary>
        [JsonProperty("ringbackTone")]
        public string RingbackTone { get; set; }

        public ConnectAction()
        {
            Action = ActionType.connect;
        }

    }
}
