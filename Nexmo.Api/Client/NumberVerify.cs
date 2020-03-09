using System;
using Nexmo.Api.Request;

namespace Nexmo.Api.ClientMethods
{
    [Obsolete("This item is rendered obsolete by version 5 - please use the new Interfaces provided by the Nexmo.Api.NexmoClient class")]
    public class NumberVerify
    {
        public Credentials Credentials { get; set; }
        public NumberVerify(Credentials creds)
        {
            Credentials = creds;
        }

        /// <summary>
        /// Number Verify: Generate and send a PIN to your user. You use the request_id in the response for the Verify Check.
        /// </summary>
        /// <param name="request">Verify request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        /// <exception cref="VerifyResponseException">Sent if response received from request was non-zero</exception>
        public Api.NumberVerify.VerifyResponse Verify(Api.NumberVerify.VerifyRequest request, Credentials creds = null)
        {
            return Api.NumberVerify.Verify(request, creds ?? Credentials);
        }

        /// <summary>
        /// Number Verify: Confirm that the PIN you received from your user matches the one sent by Nexmo as a result of your Verify Request.
        /// </summary>
        /// <param name="request">Check request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        /// <exception cref="VerifyResponseException">Sent if response received from request was non-zero</exception>
        public Api.NumberVerify.CheckResponse Check(Api.NumberVerify.CheckRequest request, Credentials creds = null)
        {
            return Api.NumberVerify.Check(request, creds ?? Credentials);
        }

        /// <summary>
        /// Number Verify: Lookup the status of one or more requests.
        /// </summary>
        /// <param name="request">Search request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public Api.NumberVerify.SearchResponse Search(Api.NumberVerify.SearchRequest request, Credentials creds = null)
        {
            return Api.NumberVerify.Search(request, creds ?? Credentials);
        }

        /// <summary>
        /// Number Verify: Control the progress of your Verify Requests.
        /// </summary>
        /// <param name="request">Control request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        /// <exception cref="VerifyResponseException">Sent if response received from request was non-zero</exception>
        public Api.NumberVerify.ControlResponse Control(Api.NumberVerify.ControlRequest request, Credentials creds = null)
        {
            return Api.NumberVerify.Control(request, creds ?? Credentials);
        }
    }
}
