using System.Collections.Generic;
using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace Nexmo.Api
{
    public static class Account
    {
        public class Balance
        {
            public decimal value { get; set; }
        }

        public class Pricing
        {
            public string country { get; set; }
            public Network[] networks { get; set; }
        }

        public class Network
        {
            public string code { get; set; }
            public string network { get; set; }
            public string mtPrice { get; set; }
            public string ranges { get; set; }
        }

        public class Settings
        {
            [JsonProperty("api-secret")]
            public string apiSecret { get; set; }
            [JsonProperty("mo-callback-url")]
            public string moCallbackUrl { get; set; }
            [JsonProperty("dr-callback-url")]
            public string drCallbackUrl { get; set; }
        }

        public class NumbersRequest
        {
            /// <summary>
            /// Optional. Page index (>0, default 1). Ex: 2
            /// </summary>
            public string index { get; set; }
            /// <summary>
            /// Optional. Page size (max 100, default 10). Ex: 25
            /// </summary>
            public string size { get; set; }
            /// <summary>
            /// Optional. A matching pattern. Ex: 33
            /// </summary>
            public string pattern { get; set; }
            /// <summary>
            /// Optional. Strategy for matching pattern. Expected values: 0 "starts with" (default), 1 "anywhere", 2 "ends with".
            /// </summary>
            public string search_pattern { get; set; }
        }

        public class NumbersResponse
        {
            public int count { get; set; }
            public List<Number> numbers { get; set; } 
        }

        public class Number
        {
            public string country { get; set; }
            public string msisdn { get; set; }
            public string type { get; set; }
            public string[] features { get; set; }
            public string moHttpUrl { get; set; }
            public string voiceCallbackType { get; set; }
            public string voiceCallbackValue { get; set; }
        }

        /// <summary>
        /// Get current account balance
        /// </summary>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns>decimal balance</returns>
        public static decimal GetBalance(Credentials creds = null)
        {
            var json = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Account),
                "/account/get-balance"),
                // TODO: using this method sig allows us to have the api auth injected at the expense of opaque code here
                new Dictionary<string, string>(),
                creds);

            var obj = JsonConvert.DeserializeObject<Balance>(json);
            return obj.value;
        }

        /// <summary>
        /// Get Nexmo pricing for the given country
        /// </summary>
        /// <param name="country">ISO 3166-1 alpha-2 country code</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns>Pricing data</returns>
        public static Pricing GetPricing(string country, Credentials creds = null)
        {
            var json = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Account),
                "/account/get-pricing/outbound/"),
                new Dictionary<string, string>
                {
                    { "country", country }
                },
                creds);

            var obj = JsonConvert.DeserializeObject<Pricing>(json);
            return obj;
        }

        /// <summary>
        /// Set account settings
        /// </summary>
        /// <param name="newsecret">New API secret</param>
        /// <param name="httpMoCallbackurlCom">An encoded URI to the webhook endpoint endpoint that handles inbound messages.</param>
        /// <param name="httpDrCallbackurlCom">An encoded URI to the webhook endpoint that handles deliver receipts (DLR).</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns>Updated settings</returns>
        public static Settings SetSettings(string newsecret = null, string httpMoCallbackurlCom = null, string httpDrCallbackurlCom = null, Credentials creds = null)
        {
            var parameters = new Dictionary<string, string>();
            if (null != newsecret)
                parameters.Add("newSecret", newsecret);
            if (null != httpMoCallbackurlCom)
                parameters.Add("moCallBackUrl", httpMoCallbackurlCom);
            if (null != httpDrCallbackurlCom)
                parameters.Add("drCallBackUrl", httpDrCallbackurlCom);

            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(Account), "/account/settings"), parameters, creds);

            // TODO: update secret in config?

            return JsonConvert.DeserializeObject<Settings>(response.JsonResponse);
        }

        /// <summary>
        /// Top-up an account that is configured for auto reload.
        /// </summary>
        /// <param name="transaction">The ID associated with your original auto-reload transaction.</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        public static void TopUp(string transaction, Credentials creds = null)
        {
            ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Account), "/account/top-up"), new Dictionary<string, string>
            {
                {"trx", transaction}
            },
            creds);

            // TODO: return response
        }

        /// <summary>
        /// Retrieve all the phone numbers associated with your account.
        /// </summary>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns>All the phone numbers associated with your account.</returns>
        public static NumbersResponse GetNumbers(Credentials creds = null)
        {
            return GetNumbers(new NumbersRequest(), creds);
        }

        /// <summary>
        /// Retrieve all the phone numbers associated with your account that match the provided filter
        /// </summary>
        /// <param name="request">Filter for account numbers list</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static NumbersResponse GetNumbers(NumbersRequest request, Credentials creds = null)
        {
            var json = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Account), "/account/numbers"), request, creds);
            return JsonConvert.DeserializeObject<NumbersResponse>(json);
        }
    }
}
