using Nexmo.Api.Request;

namespace Nexmo.Api.NumberInsights
{
    [System.Obsolete("The Nexmo.Api.NumberInsights.INumberInsightClient interface is obsolete. " +
        "References to it should be switched to the new Vonage.NumberInsights.INumberInsightClient interface.")]
    public interface INumberInsightClient
    {
        /// <summary>
        /// Provides basic number insight information about a number.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        BasicInsightResponse GetNumberInsightBasic(BasicNumberInsightRequest request, Credentials creds = null);

        /// <summary>
        /// Provides standard number insight information about a number.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        StandardInsightResponse
            GetNumberInsightStandard(StandardNumberInsightRequest request, Credentials creds = null);

        /// <summary>
        /// Provides advanced number insight information about a number synchronously, in the same way that the basic and standard endpoints do.
        /// Nexmo recommends accessing the Advanced API asynchronously
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        AdvancedInsightsResponse GetNumberInsightAdvanced(AdvancedNumberInsightRequest request,
            Credentials creds = null);

        /// <summary>
        /// Provides advanced number insight number information asynchronously using the URL specified in the callback parameter. 
        /// Vonage recommends asynchronous use of the Number Insight Advanced API, to avoid timeouts.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        AdvancedInsightsAsyncResponse GetNumberInsightAsync(AdvancedNumberInsightAsynchronousRequest request,
            Credentials creds = null);
    }
}