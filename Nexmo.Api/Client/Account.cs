using System.Collections.Generic;
using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace Nexmo.Api.ClientMethods
{
    public class Account
    {
        public Credentials Credentials { get; set; }
        public Account(Credentials creds)
        {
            Credentials = creds;
        }

        /// <summary>
        /// Get current account balance
        /// </summary>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns>decimal balance</returns>
        public decimal GetBalance(Credentials creds = null)
        {
            var json = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Api.Account),
                    "/account/get-balance"),
                // TODO: using this method sig allows us to have the api auth injected at the expense of opaque code here
                new Dictionary<string, string>(),
                creds ?? Credentials);

            var obj = JsonConvert.DeserializeObject<Api.Account.Balance>(json);
            return obj.value;
        }

        /// <summary>
        /// Get Nexmo pricing for the given country
        /// </summary>
        /// <param name="country">ISO 3166-1 alpha-2 country code</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns>Pricing data</returns>
        public Api.Account.Pricing GetPricing(string country, Credentials creds = null)
        {
            var json = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Api.Account),
                    "/account/get-pricing/outbound/"),
                new Dictionary<string, string>
                {
                    { "country", country }
                },
                creds ?? Credentials);

            var obj = JsonConvert.DeserializeObject<Api.Account.Pricing>(json);
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
        public Api.Account.Settings SetSettings(string newsecret = null, string httpMoCallbackurlCom = null, string httpDrCallbackurlCom = null, Credentials creds = null)
        {
            var parameters = new Dictionary<string, string>();
            if (null != newsecret)
                parameters.Add("newSecret", newsecret);
            if (null != httpMoCallbackurlCom)
                parameters.Add("moCallBackUrl", httpMoCallbackurlCom);
            if (null != httpDrCallbackurlCom)
                parameters.Add("drCallBackUrl", httpDrCallbackurlCom);

            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(Api.Account), "/account/settings"), parameters, creds ?? Credentials);

            // TODO: update secret in config?

            return JsonConvert.DeserializeObject<Api.Account.Settings>(response.JsonResponse);
        }

        /// <summary>
        /// Top-up an account that is configured for auto reload.
        /// </summary>
        /// <param name="transaction">The ID associated with your original auto-reload transaction.</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        public void TopUp(string transaction, Credentials creds = null)
        {
            ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Api.Account), "/account/top-up"), new Dictionary<string, string>
                {
                    {"trx", transaction}
                },
                creds ?? Credentials);

            // TODO: return response
        }

        /// <summary>
        /// Retrieve all the phone numbers associated with your account.
        /// </summary>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns>All the phone numbers associated with your account.</returns>
        public Api.Account.NumbersResponse GetNumbers(Credentials creds = null)
        {
            return GetNumbers(new Api.Account.NumbersRequest(), creds ?? Credentials);
        }

        /// <summary>
        /// Retrieve all the phone numbers associated with your account that match the provided filter
        /// </summary>
        /// <param name="request">Filter for account numbers list</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public Api.Account.NumbersResponse GetNumbers(Api.Account.NumbersRequest request, Credentials creds = null)
        {
            var json = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Api.Account), "/account/numbers"), request, creds ?? Credentials);
            return JsonConvert.DeserializeObject<Api.Account.NumbersResponse>(json);
        }
    }
}
