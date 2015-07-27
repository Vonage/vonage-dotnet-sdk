using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nexmo.Api
{
    public static class SMS
    {
        public enum sms_type
        {
            text,
            binary,
            wappush,
            unicode,
            vcal,
            vcard
        }
        public enum sms_response_codes
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
            /// <summary>
            /// Required. Sender address may be alphanumeric (Ex: from=MyCompany20). Restrictions may apply, depending on the destination.
            /// </summary>
            [JsonProperty("from")]
            public string from { get; set; }
            /// <summary>
            /// Required. Mobile number in international format, and one recipient per request. Ex: to=447525856424 or to=00447525856424 when sending to UK.
            /// </summary>
            [JsonProperty("to")]
            public string to { get; set; }
            /// <summary>
            /// Optional. This can be omitted for text (default), but is required when sending a Binary (binary), WAP Push (wappush), Unicode message (unicode), vcal (vcal) or vcard (vcard).
            /// </summary>
            [JsonProperty("type")]
            public sms_type sms_type { get; set; }
            /// <summary>
            /// Required when type='text'. Body of the text message (with a maximum length of 3200 characters), UTF-8 and URL encoded value. Ex: "Déjà vu" content would be "D%c3%a9j%c3%a0+vu"
            /// </summary>
            [JsonProperty("text")]
            public string text { get; set; }
        }

        public class SMSResponse
        {
            /// <summary>
            /// The number of parts the message was split into.
            /// </summary>
            [JsonProperty("message-count")]
            public int MessageCount { get; set; }
            /// <summary>
            /// An array containing objects for each message part.
            /// </summary>
            [JsonProperty("messages")]
            public MessagePart[] Messages { get; set; }
        }

        public class MessagePart
        {
            /// <summary>
            /// The return code.
            /// </summary>
            [JsonProperty("status")]
            public sms_response_codes status { get; set; }
            /// <summary>
            /// The ID of the message that was submitted (8 to 16 characters).
            /// </summary>
            [JsonProperty("message-id")]
            public string message_id { get; set; }
            /// <summary>
            /// The recipient number.
            /// </summary>
            [JsonProperty("to")]
            public string to { get; set; }
            /// <summary>
            /// If you set a custom reference during your request, this will return that value.
            /// </summary>
            [JsonProperty("client-ref")]
            public string client_ref { get; set; }
            /// <summary>
            /// The remaining account balance expressed in EUR
            /// </summary>
            [JsonProperty("remaining-balance")]
            public float remaining_balance { get; set; }
            /// <summary>
            /// The price charged (EUR) for the submitted message.
            /// </summary>
            [JsonProperty("message-price")]
            public float message_price { get; set; }
            /// <summary>
            /// Identifier of a mobile network MCCMNC.
            /// </summary>
            [JsonProperty("network")]
            public string network { get; set; }
            /// <summary>
            /// If an error occurred, this will explain in readable terms the error encountered.
            /// </summary>
            [JsonProperty("error-text")]
            public string error_text { get; set; }

        }

        public static SMSResponse SendSMS(SMSRequest request)
        {
            var jsonstring = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(SMSRequest), "/sms/json"), new Dictionary<string, string>()
            {
                {"from", request.from},
                {"to", request.to},
                {"type", request.sms_type.ToString()},
                {"text", request.text}
            });
            return JsonConvert.DeserializeObject<SMSResponse>(jsonstring);
        }

        // TODO: deliver receipt callback

        // TODO: inbound msg
    }
}
