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
            /// Your API Key.
            /// </summary>
            [JsonProperty("account-id")]
            public string accountId { get; set; }
            /// <summary>
            /// The sender ID the message was sent from. Could be a phone number or name.
            /// </summary>
            public string from { get; set; }
            /// <summary>
            /// The phone number the message was sent to.
            /// </summary>
            public string to { get; set; }
            /// <summary>
            /// The body of the message
            /// </summary>
            public string body { get; set; }
            /// <summary>
            /// The date and time at UTC+0 when Platform received your request in the following format: YYYY-MM-DD HH:MM:SS.
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
            /// The message type. MT (mobile terminated or outbound) or MO (mobile originated or inbound)
            /// </summary>
            public string type { get; set; }
            /// <summary>
            /// The id of the message you sent.
            /// </summary>
            [JsonProperty("message-id")]
            public string messageId { get; set; }
            /// <summary>
            /// Optional. The MCCMNC for the carrier who delivered the message.
            /// </summary>
            public string network { get; set; }

            // Specific fields for message type MT

            /// <summary>
            /// Price in Euros for a MT message
            /// </summary>
            public string price { get; set; }
            /// <summary>
            /// The date and time at UTC+0 when Platform received the delivery receipt from the carrier who delivered the MT message. This parameter is in the following format YYYY-MM-DD HH:MM:SS
            /// </summary>
            [JsonProperty("date-closed")]
            public string dateClosed { get; set; }
            /// <summary>
            /// The overall latency between date-received and date-closed in milliseconds.
            /// </summary>
            public string latency { get; set; }
            /// <summary>
            /// The internal reference you set in the request.
            /// </summary>
            [JsonProperty("client-ref")]
            public string clientRef { get; set; }
            /// <summary>
            /// The status of message-id at date-closed.
            /// </summary>
            [JsonProperty("final-status")]
            public string finalStatus { get; set; }
            /// <summary>
            /// A code that explains where the message is in the delivery process. If status is not delivered check error-code for more information. If status is accepted ignore the value of error-code
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

        /// <summary>
        /// Search for information about a single message that you sent using SMS API.
        /// </summary>
        /// <param name="id">Nexmo message ID</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static Message GetMessage(string id, Credentials creds = null)
        {
            return ApiRequest.DoGetRequest<Message>(ApiRequest.GetBaseUriFor(typeof(Search), "/search/message"), new Dictionary<string, string>
            {
                {"id", id}
            },
            creds);
        }

        /// <summary>
        /// Search for information about the messages you sent using SMS API.
        /// </summary>
        /// <param name="request">Search request with numbers</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static Messages<Message> GetMessages(SearchRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequest<Messages<Message>>(ApiRequest.GetBaseUriFor(typeof(Search), "/search/messages"), request, ApiRequest.AuthType.Query, creds);
        }

        /// <summary>
        /// Search for messages that have been rejected by Nexmo. Messages rejected by carrier do not appear.
        /// </summary>
        /// <param name="request">Search request with numbers</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static Messages<MessageBase> GetRejections(SearchRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequest<Messages<MessageBase>>(ApiRequest.GetBaseUriFor(typeof(Search), "/search/rejections"), request, ApiRequest.AuthType.Query, creds);
        }
    }
}
