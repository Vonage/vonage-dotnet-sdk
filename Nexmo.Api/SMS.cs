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
            /// <summary>
            /// An alphanumeric string giving your sender address. For example, from=MyCompany20. See our information Global messaging. This is also called the SenderID.
            /// </summary>
            public string from { get; set; }
            /// <summary>
            /// A single phone number in international format, that is E.164  . For example, to=447700900000. You can set one recipient only for each request.
            /// </summary>
            public string to { get; set; }
            /// <summary>
            /// Default value is text. Possible values are:
            ///text - for plain text SMS, you must also set the text parameter.
            ///binary - for binary SMS you must also set the udh and body parameters. Do not set the text parameter.
            ///wappush - a WAP Push  . You must also set the title and url parameters. Do not set the text or body parameters.
            ///unicode - SMS in unicode  contain fewer characters than text. Only use unicode when your SMS must contain special characters. For more information, see Encoding.
            ///vcal - send a calendar event. You send your vCal  encoded calendar event in the vcal parameter.
            ///vcard - send a business card. You send your vCard  encoded business card in the the vcard parameter.
            /// </summary>
            public string type { get; set; }
            /// <summary>
            /// The SMS body. Messages where type is text (the default) are in UTF-8 with URL encoding. You send "Déjà vu" as a text (type=text) message as long as you encode it as D%C3%A9j%C3%A0+vu. You can see the full UTF-8 character set here  . To test if your message can be URL encoded, use: http://www.url-encode-decode.com/  . If you cannot find the character you want to send in these two references, you should use unicode. For more information, see Encoding.
            /// </summary>
            public string text { get; set; }
            /// <summary>
            /// Set to 1 to receive a Delivery Receipt (DLR). To receive the DLR, you have to either:
            ///Configure a webhook endpoint in Dashboard.
            ///Set the callback parameter.
            /// </summary>
            [JsonProperty("status-report-req")]
            public string status_report_req { get; set; }
            /// <summary>
            /// A business card in vCard. You must set the type parameter to vcard.
            /// </summary>
            public string vcard { get; set; }
            /// <summary>
            /// A calendar event in vCal. You must set the type parameter to vcal.
            /// </summary>
            public string vcal { get; set; }
            /// <summary>
            /// The lifespan of this SMS in milliseconds.
            /// </summary>
            public string ttl { get; set; }
            /// <summary>
            /// The webhook endpoint the delivery receipt for this sms is sent to. This parameter overrides the webhook endpoint you set in Dashboard. This must be a fully formed URL. For example: https://mysite.com/sms_api_callback.php.
            /// </summary>
            public string callback { get; set; }
            /// <summary>
            /// Set to: 0 for Flash SMS, 1 - ME-specific, 2 - SIM / USIM specific, 3 - TE-specific See http://en.wikipedia.org/wiki/Data_Coding_Scheme  Note: Flash SMS is not fully support by all handsets and carriers.
            /// </summary>
            [JsonProperty("message-class")]
            public string message_class { get; set; }
            /// <summary>
            /// Your custom Hex encoded User Data Header (UDH)  . For example, udh=06050415811581.
            /// </summary>
            public string udh { get; set; }
            /// <summary>
            /// 	The value in decimal format for the higher level protocol  to use for this SMS. For example, to send a binary SMS to the SIM Toolkit, this would be protocol-id=127. Ensure that the value of protocol-id is aligned with udh.
            /// </summary>
            [JsonProperty("protocol-id")]
            public string protocol_id { get; set; }
            /// <summary>
            /// Hex encoded binary data. For example, body=0011223344556677.
            /// </summary>
            public string body { get; set; }
            /// <summary>
            /// The title for a wappush SMS. For example: MyCompanyIsGreat.
            /// </summary>
            public string title { get; set; }
            /// <summary>
            /// The URL your user taps to navigate to your website.
            /// </summary>
            public string url { get; set; }
            /// <summary>
            /// The availability period for a wappush type SMS in milliseconds. For example, validity=86400000. If you do not set this parameter, the default is 48 hours.
            /// </summary>
            public string validity { get; set; }
            /// <summary>
            /// If enabled, you can include a 40 character maximum length string for internal reporting/analytics. You will need to email support@nexmo.com to get this functionality enabled on your account.
            /// </summary>
            [JsonProperty("client-ref")]
            public string client_ref { get; set; }
        }

        public class SMSResponse
        {
            [JsonProperty("message-count")]
            public string message_count { get; set; }
            public System.Collections.Generic.List<SMSResponseDetail> messages { get; set; }
        }

        public class SMSResponseDetail
        {
            /// <summary>
            /// The processing status of the message.
            /// </summary>
            public string status { get; set; }
            /// <summary>
            /// The ID of the SMS that was submitted (8 to 16 characters).
            /// </summary>
            [JsonProperty("message-id")]
            public string message_id { get; set; }
            /// <summary>
            /// The phone number your request was sent to.
            /// </summary>
            public string to { get; set; }
            /// <summary>
            /// The client-ref you set in your request.
            /// </summary>
            [JsonProperty("client-ref")]
            public string client_ref { get; set; }
            /// <summary>
            /// The remaining balance in your account. The value is in EUR.
            /// </summary>
            [JsonProperty("remaining-balance")]
            public string remaining_balance { get; set; }
            /// <summary>
            /// The price charged for your request. The value is in EUR.
            /// </summary>
            [JsonProperty("message-price")]
            public string message_price { get; set; }
            /// <summary>
            /// The Mobile Country Code Mobile Network Code (MCCMNC) for the carrier of the recipient.
            /// </summary>
            public string network { get; set; }
            /// <summary>
            /// If an error occurred, this explains what happened.
            /// </summary>
            [JsonProperty("error-text")]
            public string error_text { get; set; }
        }

        public class SMSDeliveryReceipt
        {
            /// <summary>
            /// The SenderID you set in from in your request.
            /// </summary>
            public string to { get; set; }
            /// <summary>
            /// The Mobile Country Code Mobile Network Code (MCCMNC) of the carrier this phone number is registered with.
            /// </summary>
            [JsonProperty("network-code")]
            public string network_code { get; set; }
            /// <summary>
            /// The Nexmo ID for this message.
            /// </summary>
            public string messageId { get; set; }
            /// <summary>
            /// The phone number this message was sent to.
            /// </summary>
            public string msisdn { get; set; }
            /// <summary>
            /// A code that explains where the message is in the delivery process., If status is not delivered check err-code for more information. If status is accepted ignore the value of err-code. 
            /// </summary>
            public string status { get; set; }
            /// <summary>
            /// If the status is not accepted, this key will have a value
            /// </summary>
            [JsonProperty("err-code")]
            public string err_code { get; set; }
            /// <summary>
            /// How much it cost to send this message.
            /// </summary>
            public string price { get; set; }
            /// <summary>
            /// The Coordinated Universal Time (UTC) when the DLR was recieved from the carrier. The scts is in the following format: YYMMDDHHMM. For example, 1101181426 is 2011 Jan 18th 14:26.
            /// </summary>
            public string scts { get; set; }
            /// <summary>
            /// The time at UTC±00:00 when Nexmo started to push this Delivery Receipt to your webhook endpoint. The message-timestamp is in the following format YYYY-MM-DD HH:MM:SS. For example, 2020-01-01 12:00:00.
            /// </summary>
            [JsonProperty("message-timestamp")]
            public string message_timestamp { get; set; }
            /// <summary>
            /// The client-ref you set in the request.
            /// </summary>
            [JsonProperty("client-ref")]
            public string client_ref { get; set; }            
        }

        public class SMSInbound
        {
            /// <summary>
            /// Possible values are:
            ///text - standard text.
            ///unicode - URLencoded   unicode  . This is valid for standard GSM, Arabic, Chinese, double-encoded characters and so on.
            ///binary - a binary message.
            /// </summary>
            public string type { get; set; }
            /// <summary>
            /// The phone number the message was sent to. This is your virtual number.
            /// </summary>
            public string to { get; set; }
            /// <summary>
            /// The phone number that this inbound message was sent from.
            /// </summary>
            public string msisdn { get; set; }
            /// <summary>
            /// The Nexmo ID for this message.
            /// </summary>
            public string messageId { get; set; }
            /// <summary>
            /// The time at UTC±00:00  that Nexmo started to push this inbound message to your webhook endpoint. The message-timestamp is in the following format YYYY-MM-DD HH:MM:SS. For example, 2020-01-01 12:00:00.
            /// </summary>
            [JsonProperty("message-timestamp")]
            public string message_timestamp { get; set; }
            /// <summary>
            /// A unix timestamp representation of message-timestamp.
            /// </summary>
            public string timestamp { get; set; }
            /// <summary>
            /// A random string that forms part of the signed set of parameters, it adds an extra element of unpredictability into the signature for the request. You use the nonce and timestamp parameters with your shared secret to calculate and validate the signature for inbound messages.
            /// </summary>
            public string nonce { get; set; }

            // For an inbound message

            /// <summary>
            /// The message body for this inbound message.
            /// </summary>
            public string text { get; set; }
            /// <summary>
            /// The first word in the message body. This is typically used with short codes.
            /// </summary>
            public string keyword { get; set; }

            // For concatenated inbound messages

            /// <summary>
            /// True - if this is a concatenated message.
            /// </summary>
            public string concat { get; set; }
            /// <summary>
            /// The transaction reference. All parts of this message share this concat-ref.
            /// </summary>
            [JsonProperty("concat-ref")]
            public string concat_ref { get; set; }
            /// <summary>
            /// The number of parts in this concatenated message.
            /// </summary>
            [JsonProperty("concat-total")]
            public string concat_total { get; set; }
            /// <summary>
            /// The number of this part in the message. Counting starts at 1.
            /// </summary>
            [JsonProperty("concat-part")]
            public string concat_part { get; set; }

            // For binary messages

            /// <summary>
            /// The content of this message
            /// </summary>
            public string data { get; set; }
            /// <summary>
            /// The hex encoded User Data Header 
            /// </summary>
            public string udh { get; set; }
        }

        /// <summary>
        /// Send a SMS message.
        /// </summary>
        /// <param name="request">The SMS message request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static SMSResponse Send(SMSRequest request, Credentials creds = null)
        {
            if (string.IsNullOrEmpty(request.from))
            {
                request.from = Configuration.Instance.Settings["Nexmo.sender_id"];
            }

            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(SMSResponse), "/sms/json"), request, creds);

            return JsonConvert.DeserializeObject<SMSResponse>(response.JsonResponse);
        }
    }
}
