using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nexmo.Api
{
    public static class NumberInsight
    {
        public class NumberInsightRequest
        {
            public string Number { get; set; }
            public string Callback { get; set; }
        }

        public class NumberInsightRequestResponse
        {
            public string request_id { get; set; }
            public string number { get; set; }
            public string status { get; set; }
            public string error_text { get; set; }
            public string remaining_balance { get; set; }
            public string request_price { get; set; }
        }

        public class NumberInsightResponse
        {
            public string request_id { get; set; }
            public string number { get; set; }
            public string status { get; set; }
            public string status_message { get; set; }
            public string number_type { get; set; }
            public string carrier_network_code { get; set; }
            public string carrier_network_name { get; set; }
            public string carrier_country_code { get; set; }
            public string valid { get; set; }
            public string ported { get; set; }
            public string reachable { get; set; }
            public string roaming { get; set; }
            public string roaming_country_code { get; set; }
            public string roaming_network_code { get; set; }
        }

        public static NumberInsightRequestResponse Request(NumberInsightRequest request)
        {
            var jsonstring = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(NumberInsight), "/ni/json"), new Dictionary<string, string>()
            {
                {"number", request.Number},
                {"callback", request.Callback}
            });

            return JsonConvert.DeserializeObject<NumberInsightRequestResponse>(jsonstring);
        }

        public static NumberInsightResponse Response(string json)
        {
            return JsonConvert.DeserializeObject<NumberInsightResponse>(json);
        }
    }
}