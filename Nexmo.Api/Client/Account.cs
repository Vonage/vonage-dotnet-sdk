using System;
using Nexmo.Api.Request;

namespace Nexmo.Api.ClientMethods
{
    [Obsolete("This item is rendered obsolete by version 5 - please use the new Interfaces provided by the Nexmo.Api.NexmoClient class")]
    public class Account
    {
        public Credentials Credentials { get; set; }
        public Account(Credentials creds)
        {
            Credentials = creds;
        }

        /// <summary>
        /// Get current account balance data
        /// </summary>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns>Balance data</returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        [Obsolete("This item is rendered obsolete by version 5 - please use the new Interfaces provided by the Nexmo.Api.NexmoClient class")]
        public Api.Account.Balance GetBalance(Credentials creds = null)
        {
            return Api.Account.GetBalance(creds ?? Credentials);
        }

        /// <summary>
        /// Retrieve our outbound pricing for a given country
        /// </summary>
        /// <param name="country">ISO 3166-1 alpha-2 country code</param>
        /// <param name="type">The type of service you wish to retrieve data about: either sms, sms-transit or voice.</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns>Pricing data</returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        [Obsolete("This item is rendered obsolete by version 5 - please use the new Interfaces provided by the Nexmo.Api.NexmoClient class")]
        public Api.Account.Pricing GetPricing(string country, string type = null, Credentials creds = null)
        {
            return Api.Account.GetPricing(country, type, creds ?? Credentials);
        }

        /// <summary>
        /// Retrieve our outbound pricing for a given numerical prefix
        /// </summary>
        /// <param name="prefix">numerical prefix. Examples: 44,1.</param>
        /// <param name="type">The type of service you wish to retrieve data about: either sms, sms-transit or voice.</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns>Pricing data</returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        [Obsolete("This item is rendered obsolete by version 5 - please use the new Interfaces provided by the Nexmo.Api.NexmoClient class")]
        public Api.Account.Pricing GetPrefixPricing(string prefix, string type, Credentials creds = null)
        {
            return Api.Account.GetPricing(prefix, type, creds ?? Credentials);
        }

        /// <summary>
        /// Set account settings
        /// </summary>
        /// <param name="newsecret">New API secret</param>
        /// <param name="httpMoCallbackurlCom">An encoded URI to the webhook endpoint endpoint that handles inbound messages.</param>
        /// <param name="httpDrCallbackurlCom">An encoded URI to the webhook endpoint that handles deliver receipts (DLR).</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns>Updated settings</returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        [Obsolete("This item is rendered obsolete by version 5 - please use the new Interfaces provided by the Nexmo.Api.NexmoClient class")]
        public Api.Account.Settings SetSettings(string newsecret = null, string httpMoCallbackurlCom = null, string httpDrCallbackurlCom = null, Credentials creds = null)
        {
            return Api.Account.SetSettings(newsecret, httpMoCallbackurlCom, httpDrCallbackurlCom, creds ?? Credentials);
        }

        /// <summary>
        /// Top-up an account that is configured for auto reload.
        /// </summary>
        /// <param name="transaction">The ID associated with your original auto-reload transaction.</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        [Obsolete("This item is rendered obsolete by version 5 - please use the new Interfaces provided by the Nexmo.Api.NexmoClient class")]
        public void TopUp(string transaction, Credentials creds = null)
        {
            Api.Account.TopUp(transaction, creds ?? Credentials);
        }

        /// <summary>
        /// Retrieve all the phone numbers associated with your account.
        /// </summary>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns>All the phone numbers associated with your account.</returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        [Obsolete("This item is rendered obsolete by version 5 - please use the new Interfaces provided by the Nexmo.Api.NexmoClient class")]
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
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        [Obsolete("This item is rendered obsolete by version 5 - please use the new Interfaces provided by the Nexmo.Api.NexmoClient class")]
        public Api.Account.NumbersResponse GetNumbers(Api.Account.NumbersRequest request, Credentials creds = null)
        {
            return Api.Account.GetNumbers(request, creds ?? Credentials);
        }
    }
}