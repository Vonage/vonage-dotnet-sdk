using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.NumberInsights;

public interface INumberInsightClient
{
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
    Task<StandardInsightResponse> GetNumberInsightStandardAsync(StandardNumberInsightRequest request,
        Credentials creds = null);

    /// <summary>
    /// Provides advanced number insight information about a number synchronously, in the same way that the basic and standard endpoints do.
    /// Vonage recommends accessing the Advanced API asynchronously
    /// </summary>
    /// <param name="request"></param>
    /// <param name="creds"></param>
    /// <returns></returns>
    Task<AdvancedInsightsResponse> GetNumberInsightAdvancedAsync(AdvancedNumberInsightRequest request,
        Credentials creds = null);

    /// <summary>
    /// Provides advanced number insight number information asynchronously using the URL specified in the callback parameter. 
    /// Vonage recommends asynchronous use of the Number Insight Advanced API, to avoid timeouts.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="creds"></param>
    /// <returns></returns>
    Task<AdvancedInsightsAsynchronousResponse> GetNumberInsightAsynchronousAsync(
        AdvancedNumberInsightAsynchronousRequest request,
        Credentials creds = null);
}