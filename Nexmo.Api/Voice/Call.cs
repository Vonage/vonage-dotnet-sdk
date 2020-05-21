using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Nexmo.Api.Helpers;
using Nexmo.Api.Request;
using Nexmo.Api.Voice.Nccos;

namespace Nexmo.Api.Voice
{    
    public static partial class Call
    {
        public class Endpoint
        {
            /// <summary>
            /// One of the following: phone, websocket, sip
            /// </summary>
            [JsonProperty("type")]
            public string Type { get; set; }

            // phone:

            /// <summary>
            /// The phone number to connect to in E.164 format.
            /// </summary>
            public string number { get; set; }
            /// <summary>
            /// Set the digits that are sent to the user as soon as the Call is answered. The * and # digits are respected. You create pauses using p. Each pause is 500ms.
            /// </summary>
            public string dtmfAnswer { get; set; }

            // websocket/sip:

            /// <summary>
            /// The URI to the websocket you are streaming to.
            /// OR
            /// The SIP URI to the endpoint you are connecting to in the format sip:rebekka@sip.example.com.
            /// </summary>
            public string uri { get; set; }

            // websocket:

            /// <summary>
            /// The internet media type for the audio you are streaming.Possible values are: audio/l16; rate=16000
            /// </summary>
            [JsonProperty("content-type")]
            public string contentType { get; set; }

            /// <summary>
            /// A JSON object containing any metadata you want.
            /// </summary>
            public string headers { get; set; }
        }

        [JsonConverter(typeof(CallCommandConverter))]
        public class CallCommand
        {
            /// <summary>
            /// The single or mixed collection of endpoint types you connected to. Possible values.
            /// </summary>
            [JsonRequired]
            public Endpoint[] to { get; set; }
            /// <summary>
            /// The endpoint you are calling from. Possible value are the same as to.
            /// </summary>
            [JsonRequired]
            public Endpoint from { get; set; }
            
            /// <summary>
            /// This will convert to ncco as per the CallCommandConverter - it is preferable to use this over the JArray Ncco
            /// </summary>
            public Ncco Ncco { get; set; }

            /// <summary>
            /// The webhook endpoint where you provide the Nexmo Call Control Object that governs this call. As soon as your user answers a call, Platform requests this NCCO from answer_url. Use answer_method to manage the HTTP method.
            /// </summary>
            [RequiredIfAttribute("Ncco", null, ErrorMessage = "You must provide an NCCO object or an answer url")]
            public string[] answer_url { get; set; }
            /// <summary>
            /// Optional. The HTTP method used to send event information to answer_url. The default value is GET.
            /// </summary>
            public string answer_method { get; set; }
            /// <summary>
            /// Optional. Platform sends event information asynchronously to this endpoint when status changes. For more information about the values sent, see callback.
            /// </summary>
            public string[] event_url { get; set; }
            /// <summary>
            /// Optional. The HTTP method used to send event information to event_url. The default value is POST.
            /// </summary>
            public string event_method { get; set; }
            /// <summary>
            /// Optional. Configure the behavior when Nexmo detects that a destination is an answerphone.
            /// </summary>
            public string machine_detection { get; set; }
            /// <summary>
            /// Optional. Set the number of seconds that elapse before Nexmo hangs up after the call state changes to in_progress. The default value is 7200, two hours. This is also the maximum value.
            /// </summary>
            public uint length_timer { get; set; }
            /// <summary>
            /// Optional. Set the number of seconds that elapse before Nexmo hangs up after the call state changes to 'ringing'. The default value is 60, the maximum value is 120.
            /// </summary>
            public uint ringing_timer { get; set; }
        }

        public class CallCommandResponse
        {
            /// <summary>
            /// A string explaining the state of this request.
            /// </summary>
            public string message { get; set; }
            /// <summary>
            /// The unique id for this request.
            /// </summary>
            public string uuid { get; set; }
        }

        public class CallGetRecordingResponse
        {
            /// <summary>
            /// Response Status of the HTTP Request
            /// </summary>
            public HttpStatusCode Status { get; set; }

            /// <summary>
            /// Stream of bytes containg the recording file's content
            /// </summary>
            public byte[] ResultStream { get; set; }
        }

        public class CallEditCommand
        {
            /// <summary>
            /// Use one of the following options to modify the call:
            /// hangup    Terminates this call leg.
            /// mute      Mutes this call leg.
            /// unmute    Unmutes this call leg.
            /// earmuff   Prevents the recipient of this call leg from hearing other parts of the conversation.
            /// unearmuff Removes the earmuff effect from this call leg.
            /// transfer  Transfers this call leg to another NCCO, as specified by the destination parameter.
            /// </summary>
            [JsonProperty("action")]
            public string Action { get; set; }
            /// <summary>
            /// Optional. A JSON object pointing to a replacement NCCO, when action is transfer.
            /// </summary>
            [RequiredIf("Action", "transfer", ErrorMessage ="Desitnation required if transfering call")]
            [JsonProperty("destination")]
            public Destination Destination { get; set; }
        }

        public class Destination
        {
            [JsonProperty("type")]
            public string Type { get; set; } = "ncco";

            [JsonProperty("url")]
            public string[] Url { get; set; }

            [JsonProperty("ncco")]
            public Ncco Ncco { get; set; }
        }

        public class SearchFilter
        {
            /// <summary>
            /// Filter on the status of this Call.Possible values are:
            ///  started - Platform has stared the call.
            ///  ringing - the user's handset is ringing.
            ///  answered - the user has answered your call.
            ///  timeout - your user did not answer your call with ringing_timer.
            ///  machine - Platform detected an answering machine.
            ///  completed - Platform has terminated this call.
            /// </summary>
            public string status { get; set; }
            /// <summary>
            /// Return the records that occurred after this point in time.
            /// </summary>
            public DateTime? date_start { get; set; }
            /// <summary>
            /// Return the records that occurred before this point in time.
            /// </summary>
            public DateTime? date_end { get; set; }
            /// <summary>
            /// Return this amount of records in the response. The default value is 10.
            /// </summary>
            public int page_size { get; set; }
            /// <summary>
            /// Return page_size Calls from this index in the response. That is, if your request returns 300 Calls, set record_index to 5 in order to return Calls 50 to 59. The default value is 0. That is, the first page_size Calls.
            /// </summary>
            public int record_index { get; set; }
            /// <summary>
            /// Return the results in:
            ///  asc - ascending order.This is the default value.
            ///  desc - descending order.
            /// </summary>
            public string order { get; set; }
            /// <summary>
            ///  Return all the records associated with a specific Conversation.
            /// </summary>
            public string conversation_uuid { get; set; }
        }

        public class CallResponse
        {
            /// <summary>
            ///  A unique identifier for this Call.
            /// </summary>
            public string uuid { get; set; }
            /// <summary>
            /// A unique identifier for the Conversation this Call is part of.
            /// </summary>
            public string conversation_uuid { get; set; }
            /// <summary>
            /// The single or mixed collection of endpoint types you connected to.Possible value are:
            ///  phone - phone numbers in e.164 format.Options are:
            ///    number - the phone number to connect to in E.164 format.
            ///    dtmfAnswer - Set the digits that are sent to the user as soon as the Call is answered.The* and # digits are respected. You create pauses using p. Each pause is 500ms.
            /// </summary>
            public Endpoint to { get; set; }
            /// <summary>
            /// The endpoint you called from. Possible values are the same as to 
            /// </summary>
            public Endpoint from { get; set; }
            /// <summary>
            /// Possible values are outbound or inbound.
            /// </summary>
            public string direction { get; set; }
            /// <summary>
            /// The URL to download a call or conversation recording from.
            /// </summary>
            public string recording_url { get; set; }
            /// <summary>
            /// The time the Call started
            /// </summary>
            public DateTime? start_time { get; set; }
            /// <summary>
            /// The Mobile Country Code Mobile Network Code (MCCMNC) for the carrier network used to make this Call.
            /// </summary>
            public string network { get; set; }
            /// <summary>
            /// The status of the Call. Possible values are:
            ///  started - Platform has stared the Call.
            ///  ringing - the user's handset is ringing.
            ///  answered - the user has answered your Call.
            ///  timeout - your user did not answer your Call with ringing_timer.
            ///  machine - Platform detected an answering machine.
            ///  completed - Platform has terminated this Call.
            /// </summary>
            public string status { get; set; }
            /// <summary>
            /// The price per minute for this Call.
            /// </summary>
            public decimal rate { get; set; }
            /// <summary>
            /// The total price charged for this Call.
            /// </summary>
            public decimal price { get; set; }
            /// <summary>
            /// The time elapsed for the Call to take place in seconds.
            /// </summary>
            public int duration { get; set; }
            /// <summary>
            /// The time the Call ended
            /// </summary>
            public DateTime? end_time { get; set; }
        }

        public class CallList
        {
            public List<CallResponse> calls { get; set; }
        }

        /// <summary>
        /// POST /v1/calls - create an outbound SIP or PSTN Call
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static CallResponse Do(CallCommand cmd, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContent<CallResponse>("POST", ApiRequest.GetBaseUriFor(typeof(Call), "/v1/calls"), cmd, ApiRequest.AuthType.Bearer, creds);            
        }

        /// <summary>
        /// GET /v1/calls - retrieve information about all your Calls
        /// <param name="filter">Filter to search calls on</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// </summary>
        public static PaginatedResponse<CallList> List(SearchFilter filter, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<PaginatedResponse<CallList>>(ApiRequest.GetBaseUriFor(typeof(Call), "/v1/calls"), ApiRequest.AuthType.Bearer, filter, creds);
        }
        public static PaginatedResponse<CallList> List()
        {
            return List(new SearchFilter
            {
                page_size = 10
            });
        }

        /// <summary>
        /// GET /v1/calls/{uuid} - retrieve information about a single Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        public static CallResponse Get(string id, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<CallResponse>(ApiRequest.GetBaseUriFor(typeof(Call), $"/v1/calls/{id}"), ApiRequest.AuthType.Bearer, credentials:creds);
        }

        /// <summary>
        /// PUT /v1/calls/{uuid} - modify an existing Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        public static CallResponse Edit(string id, CallEditCommand cmd, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContent<CallResponse>("PUT", ApiRequest.GetBaseUriFor(typeof(Call), $"/v1/calls/{id}"), cmd, ApiRequest.AuthType.Bearer, creds);            
        }

        public static CallGetRecordingResponse GetRecording(string url, Credentials creds = null)
        {
            using (var response = ApiRequest.DoGetRequestWithJwt(new Uri(url), creds))
            {
                var readTask = response.Content.ReadAsStreamAsync();
                byte[] bytes;
                readTask.Wait();
                using (var ms = new MemoryStream())
                {
                    readTask.Result.CopyTo(ms);
                    bytes = ms.ToArray();
                }
                return new CallGetRecordingResponse()
                {
                    ResultStream = bytes,
                    Status = response.StatusCode
                };
            }
        }
    }
}