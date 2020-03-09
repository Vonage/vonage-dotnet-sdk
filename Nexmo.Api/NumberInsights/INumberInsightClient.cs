using Nexmo.Api.Request;

namespace Nexmo.Api.NumberInsights
{
    public interface INumberInsightClient
    {
        BasicInsightResponse GetNumberInsightBasic(BasicNumberInsightRequest request, Credentials creds = null);

        StandardInsightResponse
            GetNumberInsightStandard(StandardNumberInsightRequest request, Credentials creds = null);

        AdvancedInsightsResponse GetNumberInsightAdvanced(AdvancedNumberInsightSynchronousRequest request,
            Credentials creds = null);

        AsyncInsightsResponse GetNumberInsightAsync(AdvancedNumberInsightASynchronousRequest request,
            Credentials creds = null);
    }
}