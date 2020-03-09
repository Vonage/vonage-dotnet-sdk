using Newtonsoft.Json;

namespace Nexmo.Api.Messaging
{
    public class InboundSms
    {
        [JsonProperty("msisdn")]
        public string Msisdn { get; set; }
        [JsonProperty("to")]
        public string To { get; set; }
        [JsonProperty("messageId")]
        public string MessageId { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("keyword")]
        public string Keyword { get; set; }
        [JsonProperty("message-timestamp")]
        public string MessageTimestamp { get; set; }
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
        [JsonProperty("nonce")]
        public string Nonce { get; set; }
        [JsonProperty("concat")]
        public string Concat { get; set; }
        [JsonProperty("concat-ref")]
        public string ConcatRef { get; set; }
        [JsonProperty("concat-total")]
        public string ConcatTotal { get; set; }
        [JsonProperty("data")]
        public string Data { get; set; }
        [JsonProperty("udh")]
        public string Udh { get; set; }
    }
}