using Newtonsoft.Json;
using System;

namespace Vonage.Messaging
{
    public class DeliveryReceipt
    {
        /// <summary>
        /// The number the message was sent to. Numbers are specified in E.164 format.
        /// </summary>
        [JsonProperty("msisdn")]
        public string Msisdn { get; set; }

        /// <summary>
        /// The SenderID you set in from in your request.
        /// </summary>
        [JsonProperty("to")]
        public string To { get; set; }

        /// <summary>
        /// The Mobile Country Code Mobile Network Code (MCCMNC) of the carrier this phone number is registered with.
        /// </summary>
        [JsonProperty("network-code")]
        public string NetworkCode { get; set; }

        /// <summary>
        /// The Vonage ID for this message.
        /// </summary>
        [JsonProperty("messageId")]
        public string MessageId { get; set; }

        /// <summary>
        /// The cost of the message
        /// </summary>
        [JsonProperty("price")]
        public string Price { get; set; }

        /// <summary>
        /// A code that explains where the message is in the delivery process.
        /// Will be one of: delivered, expired, failed, rejected, accepted, buffered or unknown
        /// </summary>
        [JsonProperty("status")]
        public string StringStatus { get; set; }

        /// <summary>
        /// A code that explains where the message is in the delivery process.
        /// </summary>
        public DlrStatus Status
        {
            get
            {
                try
                {
                    return (DlrStatus)Enum.Parse(typeof(DlrStatus), StringStatus);
                }
                catch (Exception)
                {
                    return DlrStatus.unknown;
                }
            }
        }

        /// <summary>
        /// When the DLR was received from the carrier in the following format YYMMDDHHMM. For example, 2001011400 is at 2020-01-01 14:00
        /// </summary>
        [JsonProperty("scts")]
        public string Scts { get; set; }

        /// <summary>
        /// The status of the request. Will be a non 0 value if there has been an error, or if the status is unknown. 
        /// See the Delivery Receipt documentation for more details: https://developer.nexmo.com/messaging/sms/guides/delivery-receipts#dlr-error-codes
        /// </summary>
        [JsonProperty("err-code")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// The time when Vonage started to push this Delivery Receipt to your webhook endpoint.
        /// </summary>
        [JsonProperty("message-timestamp")]
        public string MessageTimestamp { get; set; }

        /// <summary>
        /// The API key that sent the SMS. This is useful when multiple accounts are sending webhooks to the same endpoint.
        /// </summary>
        [JsonProperty("api-key")]
        public string ApiKey { get; set; }

        /// <summary>
        /// A timestamp in Unix (seconds since the epoch) format. Only included if you have signatures enabled
        /// </summary>
        [JsonProperty("timestamp")]
        public int Timestamp { get; set; }

        /// <summary>
        /// A random string to be used when calculating the signature. Only included if you have signatures enabled
        /// </summary>
        [JsonProperty("nonce")]
        public string Nonce { get; set; }

        /// <summary>
        /// The signature to enable verification of the source of this webhook. 
        /// Please see the developer documentation for validating signatures for more information, 
        /// or use one of our published SDKs. Only included if you have signatures enabled
        /// </summary>
        [JsonProperty("sig")]
        public string Sig { get; set; }

        /// <summary>
        /// If the client-ref is set when the SMS is sent, it will be included in the delivery receipt
        /// </summary>
        [JsonProperty("client-ref")]
        public string ClientRef { get; set; }
    }
}