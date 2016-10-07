using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace Nexmo.Api.Voice
{
    public static partial class Call
    {
        public class Endpoint
        {
            public string type { get; set; }
            public string number { get; set; }
        }
        public class CallCommand
        {
            public Endpoint[] to { get; set; }
            public Endpoint from { get; set; }
            public string[] answer_url { get; set; }
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

        public class CallEditCommand
        {
            /// <summary>
            /// Use one of the following options to modify the call:
            ///  hangup - end this Call.
            /// </summary>
            public string action { get; set; }
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
            /// Possible values are outbound or inbound.
            /// </summary>
            public string direction { get; set; }
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
            /// The time the Call started
            /// </summary>
            public DateTime start_time { get; set; }
            /// <summary>
            /// The time the Call ended
            /// </summary>
            public DateTime end_time { get; set; }
            /// <summary>
            /// The Mobile Country Code Mobile Network Code (MCCMNC) for the carrier network used to make this Call.
            /// </summary>
            public string network { get; set; }
        }

        public class CallList
        {
            public List<CallResponse> calls { get; set; }
        }

        /// <summary>
        /// POST /v1/calls - create an outbound SIP or PSTN Call
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static CallResponse Do(CallCommand cmd)
        {
            var response = VersionedApiRequest.DoRequest("POST", ApiRequest.GetBaseUriFor(typeof(Call), "/v1/calls"), cmd);

            return JsonConvert.DeserializeObject<CallResponse>(response.JsonResponse);
        }

        /// <summary>
        /// GET /v1/calls - retrieve information about all your Calls
        /// </summary>
        public static PaginatedResponse<CallList> List(SearchFilter filter)
        {
            var response = VersionedApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Call), "/v1/calls"), filter);

            return JsonConvert.DeserializeObject<PaginatedResponse<CallList>>(response);
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
        public static CallResponse Get(string id)
        {
            var response = VersionedApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Call), $"/v1/calls/{id}"), new {});

            return JsonConvert.DeserializeObject<CallResponse>(response);
        }

        /// <summary>
        /// PUT /v1/calls/{uuid} - modify an existing Call
        /// </summary>
        public static CallResponse Edit(string id, CallEditCommand cmd)
        {
            var response = VersionedApiRequest.DoRequest("PUT", ApiRequest.GetBaseUriFor(typeof(Call), $"/v1/calls/{id}"), cmd);

            return JsonConvert.DeserializeObject<CallResponse>(response.JsonResponse);
        }
    }
}