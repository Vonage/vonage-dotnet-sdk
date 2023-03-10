using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vonage.Messaging
{
    public class SendSmsRequest
    {
        /// <summary>
        /// An optional string used to identify separate accounts using the SMS endpoint for billing purposes. 
        /// To use this feature, please email support@nexmo.com
        /// </summary>
        [JsonProperty("account-ref")]
        public string AccountRef { get; set; }

        /// <summary>
        /// Hex encoded binary data. Depends on type parameter having the value binary.
        /// </summary>
        [JsonProperty("body")]
        public string Body { get; set; }

        /// <summary>
        /// The webhook endpoint the delivery receipt for this sms is sent to. 
        /// This parameter overrides the webhook endpoint you set in Dashboard.
        /// </summary>
        [JsonProperty("callback")]
        public string Callback { get; set; }

        /// <summary>
        /// You can optionally include your own reference of up to 40 characters.
        /// </summary>
        [JsonProperty("client-ref")]
        public string ClientRef { get; set; }

        /// <summary>
        /// A string parameter that satisfies regulatory requirements when sending an SMS to specific countries.
        /// For more information please refer to the <see href="https://help.nexmo.com/hc/en-us/articles/115011781468">Country-Specific Outbound SMS Features</see>
        /// </summary>
        [JsonProperty("content-id")]
        public string ContentId { get; set; }

        /// <summary>
        /// A string parameter that satisfies regulatory requirements when sending an SMS to specific countries.
        /// For more information please refer to the <see href="https://help.nexmo.com/hc/en-us/articles/115011781468">Country-Specific Outbound SMS Features</see>
        /// </summary>
        [JsonProperty("entity-id")]
        public string EntityId { get; set; }

        /// <summary>
        /// The name or number the message should be sent from. 
        /// Alphanumeric senderID's are not supported in all countries, 
        /// see Global Messaging for more details. If alphanumeric, 
        /// spaces will be ignored. Numbers are specified in E.164 format.
        /// </summary>
        [JsonProperty("from")]
        public string From { get; set; }

        /// <summary>
        ///  The Data Coding Scheme value of the message
        ///  Must be one of: 0, 1, 2 or 3
        /// </summary>
        [JsonProperty("message-class")]
        public int? MessageClass { get; set; }

        /// <summary>
        /// The value of the protocol identifier to use. Ensure that the value is aligned with udh.
        /// </summary>
        [JsonProperty("protocol-id")]
        public int? ProtocolId { get; set; }

        /// <summary>
        /// Boolean indicating if you like to receive a Delivery Receipt.
        /// </summary>
        [JsonProperty("status-report-req")]
        public bool? StatusReportReq { get; set; }

        /// <summary>
        /// The body of the message being sent. If your message contains characters 
        /// that can be encoded according to the GSM Standard and Extended tables then you can set the type to text. 
        /// If your message contains characters outside this range, then you will need to set the type to unicode.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// The number that the message should be sent to. Numbers are specified in E.164 format.
        /// </summary>
        [JsonProperty("to")]
        public string To { get; set; }

        /// <summary>
        /// The duration in milliseconds the delivery of an SMS will be attempted.
        /// By default Vonage attempt delivery for 72 hours, 
        /// however the maximum effective value depends on the operator and is typically 24 - 48 hours.
        /// We recommend this value should be kept at its default or at least 30 minutes.
        /// </summary>
        [JsonProperty("ttl")]
        public int? Ttl { get; set; }

        /// <summary>
        /// The format of the message body         
        /// </summary>
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SmsType? Type { get; set; }

        /// <summary>
        /// Your custom Hex encoded User Data Header. Depends on type parameter having the value binary.
        /// </summary>
        [JsonProperty("udh")]
        public string Udh { get; set; }
    }
}