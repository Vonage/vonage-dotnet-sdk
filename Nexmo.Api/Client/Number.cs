using System.Collections.Generic;
using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace Nexmo.Api.ClientMethods
{
    public class Number
    {
        public Credentials Credentials { get; set; }
        public Number(Credentials creds)
        {
            Credentials = creds;
        }

        /// <summary>
        /// Retrieve the list of virtual numbers available for a specific country.
        /// </summary>
        /// <param name="request">Search filter</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public Api.Number.SearchResults Search(Api.Number.SearchRequest request, Credentials creds = null)
        {
            var json = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Api.Number), "/number/search/"), request, creds ?? Credentials);
            return JsonConvert.DeserializeObject<Api.Number.SearchResults>(json);
        }

        /// <summary>
        /// Rent a specific virtual number.
        /// </summary>
        /// <param name="country">ISO 3166-1 alpha-2 country code</param>
        /// <param name="number">Number to rent</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public ResponseBase Buy(string country, string number, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(Api.Number), "/number/buy"), new Dictionary<string, string>
                {
                    {"country", country},
                    {"msisdn", number}
                },
                creds ?? Credentials);

            return JsonConvert.DeserializeObject<ResponseBase>(response.JsonResponse);
        }

        /// <summary>
        /// Change the webhook endpoints associated with a rented virtual number or associate a virtual number with an Application.
        /// </summary>
        /// <param name="cmd">Update request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public ResponseBase Update(Api.Number.NumberUpdateCommand cmd, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(Api.Number), "/number/update"), cmd, creds ?? Credentials);

            return JsonConvert.DeserializeObject<ResponseBase>(response.JsonResponse);
        }

        /// <summary>
        /// Cancel your rental of a specific virtual number.
        /// </summary>
        /// <param name="country">ISO 3166-1 alpha-2 country code</param>
        /// <param name="number">The number to cancel</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public ResponseBase Cancel(string country, string number, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(Api.Number), "/number/cancel"), new Dictionary<string, string>
                {
                    {"country", country},
                    {"msisdn", number}
                },
                creds ?? Credentials);

            return JsonConvert.DeserializeObject<ResponseBase>(response.JsonResponse);
        }
    }
}