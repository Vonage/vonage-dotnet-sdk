using Nexmo.Api.Request;

namespace Nexmo.Api.NumberInsights
{
    public interface INumberInsightClient
    {
        BasicInsightResponse GetNumberInsightBasic(BasicNumberInsightRequest request, Credentials creds = null);

        StandardInsightResponse
            GetNumberInsightStandard(StandardNumberInsightRequest request, Credentials creds = null);

        AdvancedInsightsResponse GetNumberInsightAdvanced(AdvancedNumberInsightRequest request,
            Credentials creds = null);

        AdvancedInsightsAsyncResponse GetNumberInsightAsync(AdvancedNumberInsightAsynchronousRequest request,
            Credentials creds = null);
    }
}