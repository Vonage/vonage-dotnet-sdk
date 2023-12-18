using Vonage.Common.Test.Extensions;
using Vonage.NumberInsightV2.FraudCheck;
using Xunit;

namespace Vonage.Test.Unit.NumberInsightsV2.FraudCheck
{
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
}