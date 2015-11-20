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

        // TODO: settings
        
        // TODO: top up

        // TODO: numbers
    }
}
