using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Messaging
{
    public class SmsResponseMessage
    {
        /// <summary>
        /// The number the message was sent to. Numbers are specified in E.164 format.
        /// </summary>
        [JsonProperty("to")]
        public string To { get; set; }

        /// <summary>
        /// The ID of the message
        /// </summary>
        [JsonProperty("message-id")]
        public string MessageId { get; set; }

        /// <summary>
        /// The status of the message. See Troubleshooting Failed SMS.
        /// </summary>
        [JsonProperty("error-text")]
        public string ErrorText { get; set; }

        /// <summary>
        /// The status of the message. See: https://developer.nexmo.com/messaging/sms/guides/troubleshooting-sms
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Your remaining balance
        /// </summary>
        [JsonProperty("remaining-balance")]
        public string RemainingBalance { get; set; }

        /// <summary>
        /// The cost of the message
        /// </summary>
        [JsonProperty("message-price")]
        public string MessagePrice { get; set; }

        /// <summary>
        /// The ID of the network of the recipient
        /// </summary>
        [JsonProperty("network")]
        public string Network { get; set; }

        /// <summary>
        /// If a client-ref was included when sending the SMS, this field will be included and hold the value that was sent.
        /// </summary>
        [JsonProperty("account-ref")]
        public string AccountRef { get; set; }

        [JsonIgnore]
        public SmsStatusCode StatusCode 
        { 
            get
            {
                return (SmsStatusCode)int.Parse(Status);
            } 
        }
    }
}
