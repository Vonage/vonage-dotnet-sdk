using System.Collections.Generic;
using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace Nexmo.Api
{
    public static class Search
    {
        public class Messages<T> where T : MessageBase
        {
            public int count { get; set; }
            public List<T> items { get; set; } 
        }

        public class MessageBase
        {
            /// <summary>
            /// Your API Key Ex: MyKey
            /// </summary>
            [JsonProperty("account-id")]
            public string accountId { get; set; }
            /// <summary>
            /// Sender id Ex: 1234567891
            /// </summary>
            public string from { get; set; }
            /// <summary>
            /// Recipient Number Ex: 1234567890
            /// </summary>
            public string to { get; set; }
            /// <summary>
            /// Content of the message
            /// </summary>
            public string body { get; set; }
            /// <summary>
            /// Date when we have received the message YYYY-MM-DD HH:MM:SS expressed in UTC time Ex: 2011-11-15 14:34:10
            /// </summary>
            [JsonProperty("date-received")]
            public string dateReceived { get; set; }
            /// <summary>
            /// Optional. Delivery receipt error code Ex: 4
            /// </summary>
            [JsonProperty("error-code")]
            public string errorCode { get; set; }
            /// <summary>
            /// Optional. Delivery receipt error code label Ex: Call Barred
            /// </summary>
            [JsonProperty("error-code-label")]
            public string errorCodeLabel { get; set; }
        }

        public class Message : MessageBase
        {
            /// <summary>
            /// Type of message MT (Message Terminated / Outbound) or MO (Message Originated / Inbound). Ex: MT
            /// </summary>
            public string type { get; set; }
            /// <summary>
            /// ID of the sent message. Ex: 00A0B0C0
            /// </summary>
            [JsonProperty("message-id")]
            public string messageId { get; set; }
            /// <summary>
            /// Optional. Network operator MCCMNC. Ex: 23450
            /// </summary>
            public string network { get; set; }

            // Specific fields for message type MT

            /// <summary>
            /// Price for outbound message in Euro Ex: 0.035
            /// </summary>
            public string price { get; set; }
            /// <summary>
            /// Date when we have received the delivery report with a final status YYYY-MM-DD HH:MM:SS expressed in UTC time Ex: 2011-11-15 14:34:40
            /// </summary>
            [JsonProperty("date-closed")]
            public string dateClosed { get; set; }
            /// <summary>
            /// Overall latency between message submission and final delivery report, this is expressed in milliseconds Ex: 4302
            /// </summary>
            public string latency { get; set; }	
            /// <summary>
            /// Final delivery report status Ex: FAILED
            /// </summary>
            [JsonProperty("final-status")]
            public string finalStatus { get; set; }
            /// <summary>
            /// Current status of the message, this is not the final status Ex: BUFFERED
            /// </summary>
            public string status { get; set; }
        }

        public class SearchRequest
        {
            // Search by ids
            
            /// <summary>
            /// Required. A list of message ids, up to 10 Ex: ids=00A0B0C0&ids=00A0B0C1&ids=00A0B0C2
            /// </summary>
            public string ids { get; set; }

            // Search by recipient and date

            /// <summary>
            /// Required. Message date submission YYYY-MM-DD Ex: 2011-11-15
            /// </summary>
            public string date { get; set; }
            /// <summary>
            /// Required. A recipient number Ex: 1234567890
            /// </summary>
            public string to { get; set; }
        }

        public static Message GetMessage(string id)
        {
            var json = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Search), "/search/message"), new Dictionary<string, string>
            {
                {"id", id}
            });
            return JsonConvert.DeserializeObject<Message>(json);
        }

        public static Messages<Message> GetMessages(SearchRequest request)
        {
            var json = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Search), "/search/messages"), request);
            return JsonConvert.DeserializeObject<Messages<Message>>(json);
        }

        public static Messages<MessageBase> GetRejections(SearchRequest request)
        {
            var json = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Search), "/search/rejections"), request);
            return JsonConvert.DeserializeObject<Messages<MessageBase>>(json);
        }
    }
}
