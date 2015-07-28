using Newtonsoft.Json;

namespace Nexmo.Api
{
    public static class SMS
    {
        public class SMSRequest
        {
            public string from { get; set; }
            public string to { get; set; }
            public string type { get; set; }
            public string text { get; set; }
            [JsonProperty("status-report-req")]
            public string status_report_req { get; set; }
            [JsonProperty("client-ref")]
            public string client_ref { get; set; }
            [JsonProperty("network-code")]
            public string network_code { get; set; }
            public string vcar { get; set; }
            public string vcal { get; set; }
            public string ttl { get; set; }
            [JsonProperty("message-class")]
            public string message_class { get; set; }
            public string udh { get; set; }
            public string body { get; set; }
        }

        public class SMSResponse
        {
            [JsonProperty("message-count")]
            public string message_count { get; set; }
            public System.Collections.Generic.List<SMSResponseDetail> messages { get; set; }
        }

        public class SMSResponseDetail
        {
            public string status { get; set; }
            [JsonProperty("message-id")]
            public string message_id { get; set; }
            public string to { get; set; }
            [JsonProperty("client-ref")]
            public string client_ref { get; set; }
            [JsonProperty("remaining-balance")]
            public string remaining_balance { get; set; }
            [JsonProperty("message-price")]
            public string message_price { get; set; }
            public string network { get; set; }
            [JsonProperty("error-text")]
            public string error_text { get; set; }
        }

        public static SMSResponse SendSMS(SMSRequest request)
        {

            if (string.IsNullOrEmpty(request.from))
            {
                request.from = System.Configuration.ConfigurationManager.AppSettings["Nexmo.sender_id"];
            }

            var jsonstring = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(SMSResponse), "/sms/json"), request);

            return JsonConvert.DeserializeObject<SMSResponse>(jsonstring);
        }
        
        
        
        // TODO: send message
        
        // TODO: deliver receipt callback

        // TODO: inbound msg
    }
}
