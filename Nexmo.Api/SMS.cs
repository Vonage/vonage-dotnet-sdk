using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace Nexmo.Api
{
    public static class SMS
    {
        public enum SmsType
        {
            text,
            binary,
            wappush,
            unicode,
            vcal,
            vcard
        }

        public enum SmsResponseCodes
        {
            /// <summary>
            /// The message was successfully accepted for delivery by Nexmo
            /// </summary>
            success = 0,
            /// <summary>
            /// You have exceeded the submission capacity allowed on this account, please back-off and retry
            /// </summary>
            throttled = 1,
            /// <summary>
            /// Your request is incomplete and missing some mandatory parameters
            /// </summary>
            missing_params = 2,
            /// <summary>
            /// The value of one or more parameters is invalid
            /// </summary>
            invalid_params = 3,
            /// <summary>
            /// The api_key / api_secret you supplied is either invalid or disabled
            /// </summary>
            invalid_credentials = 4,
            /// <summary>
            /// An error has occurred in the Nexmo platform whilst processing this message
            /// </summary>
            internal_error = 5,
            /// <summary>
            /// The Nexmo platform was unable to process this message. For example due to an un-recognized destination number prefix
            /// </summary>
            invalid_message = 6,
            /// <summary>
            /// The number you are trying to submit to is blacklisted and may not receive messages
            /// </summary>
            number_barred = 7,
            /// <summary>
            /// The api_key you supplied is for an account that has been barred from submitting messages
            /// </summary>
            partner_account_barred = 8,
            /// <summary>
            /// Your pre-pay account does not have sufficient credit to process this message
            /// </summary>
            partner_quota_exceeded = 9,
            /// <summary>
            /// This account is not provisioned for REST submission, you should use SMPP instead
            /// </summary>
            account_rest_not_enabled = 11,
            /// <summary>
            /// Applies to Binary submissions, where the length of the UDH and the message body combined exceed 140 octets
            /// </summary>
            message_too_long = 12,
            /// <summary>
            /// Message was not submitted because there was a communication failure
            /// </summary>
            communication_failed = 13,
            /// <summary>
            /// Message was not submitted due to a verification failure in the submitted signature
            /// </summary>
            invalid_signature = 14,
            /// <summary>
            /// The sender address (from parameter) was not allowed for this message. Restrictions may apply depending on the destination see our FAQs
            /// </summary>
            invalid_sender_address = 15,
            /// <summary>
            /// The ttl parameter values is invalid
            /// </summary>
            invalid_ttl = 16,
            /// <summary>
            /// Your request makes use of a facility that is not enabled on your account
            /// </summary>
            facility_not_allowed = 19,
            /// <summary>
            /// The message class value supplied was out of range (0 - 3)
            /// </summary>
            invalid_message_class = 20,
            /// <summary>
            /// The destination number is not in your pre-approved destination list (trial accounts only).
            /// </summary>
            non_whitelisted_destination = 29
        }

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
            public string vcard { get; set; }
            public string vcal { get; set; }
            public string ttl { get; set; }
            public string callback { get; set; }
            [JsonProperty("message-class")]
            public string message_class { get; set; }
            public string udh { get; set; }
            [JsonProperty("protocol-id")]
            public string protocol_id { get; set; }
            public string body { get; set; }
            public string title { get; set; }
            public string url { get; set; }
            public string validity { get; set; }
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

        public class SMSDeliveryReceipt
        {
            public string to { get; set; }
            [JsonProperty("network-code")]
            public string network_code { get; set; }
            public string messageId { get; set; }
            public string msisdn { get; set; }
            public string status { get; set; }
            [JsonProperty("err-code")]
            public string err_code { get; set; }
            public string price { get; set; }
            public string scts { get; set; }
            [JsonProperty("message-timestamp")]
            public string message_timestamp { get; set; }
            [JsonProperty("client-ref")]
            public string client_ref { get; set; }            
        }

        public class SMSInbound
        {
            public string type { get; set; }
            public string to { get; set; }
            public string msisdn { get; set; }
            public string messageId { get; set; }
            [JsonProperty("message-timestamp")]
            public string message_timestamp { get; set; }

            public string text { get; set; }
            public string keyword { get; set; }

            public string concat { get; set; }
            [JsonProperty("concat-ref")]
            public string concat_ref { get; set; }
            [JsonProperty("concat-total")]
            public string concat_total { get; set; }
            [JsonProperty("concat-part")]
            public string concat_part { get; set; }

            public string data { get; set; }
            public string udh { get; set; }
        }

        public static SMSResponse Send(SMSRequest request)
        {
            if (string.IsNullOrEmpty(request.from))
            {
                request.from = Configuration.Instance.Settings["Nexmo.sender_id"];
            }

            var jsonstring = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(SMSResponse), "/sms/json"), request);

            return JsonConvert.DeserializeObject<SMSResponse>(jsonstring);
        }
    }
}
