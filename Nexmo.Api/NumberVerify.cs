using Nexmo.Api.Request;
using System;
using System.Collections.Generic;

namespace Nexmo.Api
{
    public static class NumberVerify
    {
        public class VerifyResponseBase
        {
            /// <summary>
            /// If status is not 0, this explains the error encountered.
            /// </summary>
            public string status { get; set; }

            /// <summary>
            /// The response code that explains how your request proceeded. (verify_response_codes: somevalue)
            /// </summary>
            public string error_text { get; set; }
        }

        public class VerifyRequest
        {
            /// <summary>
            /// The mobile or landline phone number to verify. Unless you are setting country explicitly, this number must be in E.164  format. For example, 447700900000.
            /// </summary>
            public string number { get; set; }
            /// <summary>
            /// If do not set number in international format or you are not sure if number is correctly formatted, set country with the two-character country code. For example, GB, US. Verify works out the international phone number for you.
            /// </summary>
            public string country { get; set; }
            /// <summary>
            /// The name of the company or App you are using Verify for. This 18 character alphanumeric string is used in the body of Verify message. For example: "Your brand PIN is ..".
            /// </summary>
            public string brand { get; set; }
            /// <summary>
            /// An 11 character alphanumeric string to specify the SenderID for SMS sent by Verify. Depending on the destination of the phone number you are applying, restrictions may apply. By default, sender_id is VERIFY.
            /// </summary>
            public string sender_id { get; set; }
            /// <summary>
            /// The length of the PIN. Possible values are 6 or 4 characters. The default value is 4.
            /// </summary>
            public string code_length { get; set; }
            /// <summary>
            /// By default, the SMS or text-to-speech (TTS) message is generated in the locale that matches the number. For example, the text message or TTS message for a 33* number is sent in French. Use this parameter to explicitly control the language, accent and gender used for the Verify request. The default language is en-us.
            /// </summary>
            public string lg { get; set; }
            /// <summary>
            /// Restrict verification to a certain network type. Possible values are:
            ///All (Default)
            ///Mobile
            ///Landline
            ///
            ///Note: contact support@nexmo.com to enable this feature.
            /// </summary>
            public string require_type { get; set; }
            /// <summary>
            /// The PIN validity time from generation. This is an integer value between 60 and 3600 seconds. The default is 300 seconds. When specified together, pin_expiry must be an integer multiple of next_event_wait. Otherwise, pin_expiry is set to equal next_event_wait. For example:
            ///pin_expiry = 360 seconds, so next_event_wait = 120 seconds - all three attempts have the same PIN.
            ///pin_expiry = 240 seconds, so next_event_wait = 120 seconds - 1st and 2nd attempts have the same PIN, third attempt has a different PIN.
            ///pin_expiry = 120 (or 200 or 400 seconds) - each attempt has a different PIN.
            /// </summary>
            public string pin_expiry { get; set; }
            /// <summary>
            /// An integer value between 60 and 900 seconds inclusive that specifies the wait time between attempts to deliver the PIN. Verify calculates the default value based on the average time taken by users to complete verification.
            /// </summary>
            public string next_event_wait { get; set; }
            /// <summary>
            /// The workflow ID for selecting verification workflow - choose between 1 and 5 full guide https://developer.nexmo.com/verify/guides/workflows-and-events
            /// workflow_id = 1, (Default Workflow): SMS -> TTS -> TTS
            /// workflow_id = 2, SMS -> SMS -> TTS
            /// workflow_id = 3, TTS -> TTS
            /// workflow_id = 4, SMS -> SMS
            /// workflow_id = 5, SMS -> TTS
            /// </summary>
            public string workflow_id { get; set; }
        }

        public class VerifyResponse : VerifyResponseBase
        {
            /// <summary>
            /// The unique ID of the Verify request you sent. The value of request_id is up to 32 characters long. You use this request_id for the Verify Check.
            /// </summary>
            public string request_id { get; set; }
        }

        /// <summary>
        /// Number Verify: Generate and send a PIN to your user. You use the request_id in the response for the Verify Check.
        /// </summary>
        /// <param name="request">Verify request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static VerifyResponse Verify(VerifyRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoRequest<VerifyResponse>(ApiRequest.GetBaseUriFor(typeof(NumberVerify), "/verify/json"), request, creds);
            ValidateVerifyResponse(response);
            return response;
        }

        public class CheckRequest
        {
            /// <summary>
            /// The identifier of the Verify request to check. This is the request_id you received in the Verify Request response.
            /// </summary>
            public string request_id { get; set; }
            /// <summary>
            /// The PIN given by your user.
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// The IP Address used by your user when they entered the PIN. Nexmo uses this information to identify fraud and spam patterns across our customer base. This ultimately benefits all Nexmo customers.
            /// </summary>
            public string ip_address { get; set; }
        }

        public class CheckResponse : VerifyResponseBase
        {
            /// <summary>
            /// The identifier of the SMS message-id.
            /// </summary>
            public string event_id { get; set; }
            /// <summary>
            /// The price charged for this Verify request.
            /// </summary>
            public string price { get; set; }
            /// <summary>
            /// Currency code.
            /// </summary>
            public string currency { get; set; }
        }

        /// <summary>
        /// Number Verify: Confirm that the PIN you received from your user matches the one sent by Nexmo as a result of your Verify Request.
        /// </summary>
        /// <param name="request">Check request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static CheckResponse Check(CheckRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoRequest<CheckResponse>(ApiRequest.GetBaseUriFor(typeof(NumberVerify), "/verify/check/json"),
                new Dictionary<string, string>
                {
                    {"request_id", request.request_id},
                    {"code", request.code}
                },
                creds);
            ValidateVerifyResponse(response);
            return response;
        }

        public class SearchRequest
        {
            /// <summary>
            /// The request_id you received in the Verify Request Response.
            /// </summary>
            public string request_id { get; set; }
            /// <summary>
            /// More than one request_id. Each request_id is a new parameter in the Verify Search request.
            /// </summary>
            public string request_ids { get; set; }
        }

        public class SearchResponse : VerifyResponseBase
        {
            /// <summary>
            /// The request_id you received in the Verify Request Response and used in the Verify Search request.
            /// </summary>
            public string request_id { get; set; }
            /// <summary>
            /// The Account ID the request was for.
            /// </summary>
            public string account_id { get; set; }
            /// <summary>
            /// The phone number this Verify Request was made for.
            /// </summary>
            public string number { get; set; }
            /// <summary>
            /// The price charged for this Verify Request.
            /// </summary>
            public string price { get; set; }
            /// <summary>
            /// The currency code.
            /// </summary>
            public string currency { get; set; }
            /// <summary>
            /// The sender_id you provided in the Verify Request.
            /// </summary>
            public string sender_id { get; set; }
            /// <summary>
            /// The date and time the Verification Request was submitted. This response parameter is in the following format YYYY-MM-DD HH:MM:SS. For example, 2012-04-05 09:22:57.
            /// </summary>
            public string date_submitted { get; set; }
            /// <summary>
            /// The date and time the Verification Request was completed. This response parameter is in the following format YYYY-MM-DD HH:MM:SS. For example, 2012-04-05 09:22:57.
            /// </summary>
            public string date_finalized { get; set; }
            /// <summary>
            /// Time first attempt was made. This response parameter is in the following format YYYY-MM-DD HH:MM:SS. For example, 2012-04-05 09:22:57.
            /// </summary>
            public string first_event_date { get; set; }
            /// <summary>
            /// Time last attempt was made. This response parameter is in the following format YYYY-MM-DD HH:MM:SS. For example, 2012-04-05 09:22:57.
            /// </summary>
            public string last_event_date { get; set; }
            /// <summary>
            /// The list of checks made for this verification and their outcomes.
            /// </summary>
            public CheckObj[] checks { get; set; }
        }

        public class CheckObj
        {
            public string date_received { get; set; }
            public string code { get; set; }
            public string status { get; set; }
            public string ip_address { get; set; }
        }

        /// <summary>
        /// Number Verify: Lookup the status of one or more requests.
        /// </summary>
        /// <param name="request">Search request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static SearchResponse Search(SearchRequest request, Credentials creds = null)
        {
            return ApiRequest.DoRequest<SearchResponse>(ApiRequest.GetBaseUriFor(typeof(NumberVerify), "/verify/search/json"), new Dictionary<string, string>()
            {
                {"request_id", request.request_id},
                {"request_ids", request.request_ids}
            },
            creds);
        }

        public class ControlRequest
        {
            /// <summary>
            /// The request_id you received in the Verify Request Response
            /// </summary>
            public string request_id { get; set; }
            /// <summary>
            /// Change the command workflow. Supported values are:
            ///   cancel - stop the request
            ///   trigger_next_event - advance the request to the next part of the process.
            /// </summary>
            public string cmd { get; set; }
        }

        public class ControlResponse : VerifyResponseBase
        {
            /// <summary>
            /// The cmd you sent in the request.
            /// </summary>
            public string command { get; set; }
        }

        /// <summary>
        /// Number Verify: Control the progress of your Verify Requests.
        /// </summary>
        /// <param name="request">Control request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static ControlResponse Control(ControlRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoRequest<ControlResponse>(ApiRequest.GetBaseUriFor(typeof(NumberVerify), "/verify/control/json"), request, creds);
            ValidateVerifyResponse(response);
            return response;
        }

        /// <summary>
        /// Interrogates the status of Verify Response - throws an exception if the response is non-zero
        /// </summary>
        /// <param name="response"></param>
        /// <exception cref="VerifyResponseException">throws an exception if the status is non-0</exception>
        public static void ValidateVerifyResponse(VerifyResponseBase response)
        {
            if (string.Equals(response.status,"0"))
            {
                throw new VerifyResponseException($"Error encountered during verify request - API returned a status of: {response.status} with an error text of: {response.error_text}")
                {
                    ErrorText = response.error_text,
                    Status = response.status
                };
            }
        }
    }
    public class VerifyResponseException : Exception
    {
        public VerifyResponseException(string message) : base(message) { }
        public string Status { get; set; }
        public string ErrorText { get; set; }
    }
}
