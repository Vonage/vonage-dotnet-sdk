using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.NumberInsights
{
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
        /// Provides basic number insight information about a number.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        Task<BasicInsightResponse> GetNumberInsightBasicAsync(BasicNumberInsightRequest request, Credentials creds = null);

        /// <summary>
        /// Provides standard number insight information about a number.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        StandardInsightResponse
            GetNumberInsightStandard(StandardNumberInsightRequest request, Credentials creds = null);

        /// <summary>
        /// Provides standard number insight information about a number.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        Task<StandardInsightResponse>
            GetNumberInsightStandardAsync(StandardNumberInsightRequest request, Credentials creds = null);

        /// <summary>
        /// Provides advanced number insight information about a number synchronously, in the same way that the basic and standard endpoints do.
        /// Vonage recommends accessing the Advanced API asynchronously
        /// 
        /// NOTE: This is the synchrnous version of the synchrnous GetNumberInsightAdvanced api call
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        AdvancedInsightsResponse GetNumberInsightAdvanced(AdvancedNumberInsightRequest request,
            Credentials creds = null);

        /// <summary>
        /// Provides advanced number insight information about a number synchronously, in the same way that the basic and standard endpoints do.
        /// Vonage recommends accessing the Advanced API asynchronously
        /// 
        /// NOTE: This is the asynchrnous version of the synchrnous GetNumberInsightAdvanced API
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        Task<AdvancedInsightsResponse> AsyncGetNumberInsightAdvanced(AdvancedNumberInsightRequest request,
            Credentials creds = null);

        /// <summary>
        /// Provides advanced number insight number information asynchronously using the URL specified in the callback parameter. 
        /// Vonage recommends asynchronous use of the Number Insight Advanced API, to avoid timeouts.
        /// 
        /// NOTE: This is the synchrnous version of the async Get Advanced Insights API
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        AdvancedInsightsAsyncResponse GetNumberInsightAsync(AdvancedNumberInsightAsynchronousRequest request,
            Credentials creds = null);

        /// <summary>
        /// Provides advanced number insight number information asynchronously using the URL specified in the callback parameter. 
        /// Vonage recommends asynchronous use of the Number Insight Advanced API, to avoid timeouts.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        Task<AdvancedInsightsAsyncResponse> AsyncGetNumberInsightAsync(AdvancedNumberInsightAsynchronousRequest request,
            Credentials creds = null);
    }
}