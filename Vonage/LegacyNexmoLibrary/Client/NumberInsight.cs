using System;
using Nexmo.Api.Request;

namespace Nexmo.Api.ClientMethods
{
    [System.Obsolete("This item is rendered obsolete by version 5 - please use the new Interfaces provided by the Vonage.VonageClient class")]
    public class NumberInsight
    {
        public Credentials Credentials { get; set; }
        public NumberInsight(Credentials creds)
        {
            Credentials = creds;
        }

        /// <summary>
        /// Performs basic semantic checks on given phone number.
        /// </summary>
        /// <param name="request">NI request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        /// <exception cref="NumberInsightResponseException">thrown if response contains non-zero status</exception>
        public Api.NumberInsight.NumberInsightBasicResponse RequestBasic(Api.NumberInsight.NumberInsightRequest request, Credentials creds = null)
        {
            return Api.NumberInsight.RequestBasic(request, creds ?? Credentials);
        }

        /// <summary>
        /// Identifies the phone number type and, for mobile phone numbers, the network it is registered with.
        /// </summary>
        /// <param name="request">NI standard request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        /// <exception cref="NumberInsightResponseException">thrown if response contains non-zero status</exception>
        public Api.NumberInsight.NumberInsightStandardResponse RequestStandard(Api.NumberInsight.NumberInsightRequest request, Credentials creds = null)
        {
            return Api.NumberInsight.RequestStandard(request, creds ?? Credentials);
        }

        /// <summary>
        /// Retrieve validity, roaming, and reachability information about a mobile phone number.
        /// </summary>
        /// <param name="request">NI advenced request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        /// <exception cref="NumberInsightResponseException">thrown if response contains non-zero status</exception>
        public Api.NumberInsight.NumberInsightAdvancedResponse RequestAdvanced(Api.NumberInsight.NumberInsightRequest request, Credentials creds = null)
        {
            return Api.NumberInsight.RequestAdvanced(request, creds ?? Credentials);
        }

        /// <summary>
        /// Retrieve validity, roaming, and reachability information about a mobile phone number via a webhook
        /// </summary>
        /// <param name="request">NI advenced request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public Api.NumberInsight.NumberInsightAsyncRequestResponse RequestAsync(Api.NumberInsight.NumberInsightAsyncRequest request, Credentials creds = null)
        {
            return Api.NumberInsight.RequestAsync(request, creds ?? Credentials);
        }
    }
}