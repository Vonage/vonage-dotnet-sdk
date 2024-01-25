using Vonage.NumberInsightV2.FraudCheck;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.NumberInsightsV2.FraudCheck;

public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        FraudCheckRequest
            .Build()
            .WithPhone("447009000000")
            .WithFraudScore()
            .WithSimSwap()
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess("/v2/ni");
}