using System.Collections.Generic;
using System.Configuration;
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

        public static decimal GetBalance()
        {
            var json = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Account),
                "/account/get-balance/" +
                ConfigurationManager.AppSettings["Nexmo.api_key"] + "/" + ConfigurationManager.AppSettings["Nexmo.api_secret"]));

            var obj = JsonConvert.DeserializeObject<Balance>(json);
            return obj.value;
        }

        public static Pricing GetPricing(string country)
        {
            var json = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Account),
                "/account/get-pricing/outbound/" +
                ConfigurationManager.AppSettings["Nexmo.api_key"] + "/" + ConfigurationManager.AppSettings["Nexmo.api_secret"] +
                "/" + country));

            var obj = JsonConvert.DeserializeObject<Pricing>(json);
            return obj;
        }

        public static Settings SetSettings(string newsecret = null, string httpMoCallbackurlCom = null, string httpDrCallbackurlCom = null)
        {
            var parameters = new Dictionary<string, string>();
            if (null != newsecret)
                parameters.Add("newSecret", newsecret);
            if (null != httpMoCallbackurlCom)
                parameters.Add("moCallBackUrl", httpMoCallbackurlCom);
            if (null != httpDrCallbackurlCom)
                parameters.Add("drCallBackUrl", httpDrCallbackurlCom);

            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(Account), "/account/settings"), parameters);

            // TODO: update secret?

            return JsonConvert.DeserializeObject<Settings>(response.JsonResponse);
        }

        public static void TopUp(string transaction)
        {
            ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Account), "/account/top-up"), new Dictionary<string, string>
            {
                {"trx", transaction}
            });

            // TODO: return response
        }

        public static NumbersResponse GetNumbers()
        {
            return GetNumbers(new NumbersRequest());
        }

        public static NumbersResponse GetNumbers(NumbersRequest request)
        {
            var json = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Account), "/account/numbers"), request);
            return JsonConvert.DeserializeObject<NumbersResponse>(json);
        }
    }
}
