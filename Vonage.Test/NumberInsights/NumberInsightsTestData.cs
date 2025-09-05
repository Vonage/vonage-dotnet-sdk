#region
using Vonage.NumberInsights;
#endregion

namespace Vonage.Test.NumberInsights;

internal static class NumberInsightsTestData
{
    internal static BasicNumberInsightRequest CreateBasicRequest() =>
        new BasicNumberInsightRequest
        {
            Number = "15555551212",
        };

    internal static BasicNumberInsightRequest CreateBasicRequestWithCountry() =>
        new BasicNumberInsightRequest
        {
            Country = "GB",
            Number = "15555551212",
        };

    internal static StandardNumberInsightRequest CreateStandardRequest() =>
        new StandardNumberInsightRequest
        {
            Number = "15555551212",
        };

    internal static StandardNumberInsightRequest CreateStandardRequestWithCnam() =>
        new StandardNumberInsightRequest
        {
            Cnam = true,
            Country = "GB",
            Number = "15555551212",
        };

    internal static AdvancedNumberInsightRequest CreateAdvancedRequest() =>
        new AdvancedNumberInsightRequest
        {
            Number = "15555551212",
        };

    internal static AdvancedNumberInsightRequest CreateAdvancedRequestWithAllProperties() =>
        new AdvancedNumberInsightRequest
        {
            Cnam = true,
            Country = "GB",
            Number = "15555551212",
            Ip = "123.0.0.255",
        };

    internal static AdvancedNumberInsightRequest CreateAdvancedRequestWithRealTimeData() =>
        new AdvancedNumberInsightRequest
        {
            Cnam = true,
            Country = "GB",
            Number = "15555551212",
            Ip = "123.0.0.255",
            RealTimeData = true,
        };

    internal static AdvancedNumberInsightRequest CreateAdvancedRequestForRoaming() =>
        new AdvancedNumberInsightRequest
        {
            Number = "447700900000",
        };

    internal static AdvancedNumberInsightAsynchronousRequest CreateAsyncRequest() =>
        new AdvancedNumberInsightAsynchronousRequest
        {
            Number = "15555551212",
            Callback = "https://example.com/callback",
        };

    internal static AdvancedNumberInsightAsynchronousRequest CreateAsyncRequestWithAllProperties() =>
        new AdvancedNumberInsightAsynchronousRequest
        {
            Cnam = true,
            Country = "GB",
            Number = "15555551212",
            Ip = "123.0.0.255",
            Callback = "https://example.com/callback",
        };
}