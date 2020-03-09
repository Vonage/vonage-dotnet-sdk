using System;
using Nexmo.Api.Request;

namespace Nexmo.Api.ClientMethods
{
    [Obsolete("This item is rendered obsolete by version 5 - please use the new Interfaces provided by the Nexmo.Api.NexmoClient class")]
    public class Number
    {
        public Credentials Credentials { get; set; }
        public Number(Credentials creds)
        {
            Credentials = creds;
        }

        /// <summary>
        /// List Numbers available in your account
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public Api.Number.SearchResults ListOwnNumbers(Api.Number.SearchRequest request, Credentials creds = null)
        {
            return Api.Number.ListOwnNumbers(request, creds ?? Credentials);
        }

        /// <summary>
        /// Retrieve the list of virtual numbers available for a specific country.
        /// </summary>
        /// <param name="request">Search filter</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public Api.Number.SearchResults Search(Api.Number.SearchRequest request, Credentials creds = null)
        {
            return Api.Number.Search(request, creds ?? Credentials);
        }

        /// <summary>
        /// Rent a specific virtual number.
        /// </summary>
        /// <param name="country">ISO 3166-1 alpha-2 country code</param>
        /// <param name="number">Number to rent</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public ResponseBase Buy(string country, string number, Credentials creds = null)
        {
            return Api.Number.Buy(country, number, creds ?? Credentials);
        }

        /// <summary>
        /// Change the webhook endpoints associated with a rented virtual number or associate a virtual number with an Application.
        /// </summary>
        /// <param name="cmd">Update request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public ResponseBase Update(Api.Number.NumberUpdateCommand cmd, Credentials creds = null)
        {
            return Api.Number.Update(cmd, creds ?? Credentials);
        }

        /// <summary>
        /// Cancel your rental of a specific virtual number.
        /// </summary>
        /// <param name="country">ISO 3166-1 alpha-2 country code</param>
        /// <param name="number">The number to cancel</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public ResponseBase Cancel(string country, string number, Credentials creds = null)
        {
            return Api.Number.Cancel(country, number, creds ?? Credentials);
        }
    }
}