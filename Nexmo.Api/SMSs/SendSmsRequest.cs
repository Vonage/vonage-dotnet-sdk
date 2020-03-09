using Newtonsoft.Json;

namespace Nexmo.Api.Messaging
{
    public class SendSmsRequest
    {
        [JsonProperty("from")]
        public string From { get; set; }
        [JsonProperty("to")]
        public string To { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("ttl")]
        public int? Ttl { get; set; }
        [JsonProperty("status-report-req")]
        public bool? StatusReportReq { get; set; }
        [JsonProperty("callback")]
        public string Callback { get; set; }
        [JsonProperty("message-class")]
        public int? MessageClass { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("vcard")]
        public string Vcard { get; set; }
        [JsonProperty("vcal")]
        public string Vcal { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
        [JsonProperty("udh")]
        public string Udh { get; set; }
        [JsonProperty("protocol-id")]
        public int? ProtocolId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("validity")]
        public string Validity { get; set; }
        [JsonProperty("client-ref")]
        public string ClientRef { get; set; }
        [JsonProperty("account-ref")]
        public string AccountRef { get; set; }
    }
}