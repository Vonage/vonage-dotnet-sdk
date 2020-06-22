using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nexmo.Api.Common;

namespace Nexmo.Api.Voice
{    
    public class CallSearchFilter
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
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Return the records that occurred after this point in time.
        /// Times must be in UTC, they will not be converted.
        /// </summary>
        [JsonProperty("date_start")]
        [JsonConverter(typeof(PageListDateTimeConverter))]
        public DateTime? DateStart { get; set; }

        /// <summary>
        /// Return the records that occurred before this point in time.
        /// Times must be in UTC, they will not be converted.
        /// </summary>
        [JsonProperty("date_end")]
        [JsonConverter(typeof(PageListDateTimeConverter))]
        public DateTime? DateEnd { get; set; }
        
        /// <summary>
        /// Return this amount of records in the response. The default value is 10.
        /// </summary>
        [JsonProperty("page_size")]
        public int? PageSize { get; set; }

        /// <summary>
        /// Return page_size Calls from this index in the response. That is, if your request returns 300 Calls, set record_index to 5 in order to return Calls 50 to 59. The default value is 0. That is, the first page_size Calls.
        /// </summary>
        [JsonProperty("record_index")]
        public int? RecordIndex { get; set; }

        /// <summary>
        /// Return the results in:
        ///  asc - ascending order.This is the default value.
        ///  desc - descending order.
        /// </summary>
        [JsonProperty("order")]
        public string Order { get; set; }
        
        /// <summary>
        ///  Return all the records associated with a specific Conversation.
        /// </summary>
        [JsonProperty("conversation_uuid")]
        public string ConversationUuid { get; set; }
    }
}
