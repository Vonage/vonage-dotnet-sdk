#region
using Vonage.NumberInsightV2.FraudCheck;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.NumberInsightsV2.FraudCheck;

[Trait("Category", "Request")]
[Trait("Product", "NumberInsightsV2")]
public class RequestTest
{
    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        FraudCheckRequest
            .Build()
            .WithPhone("447009000000")
            .WithFraudScore()
            .WithSimSwap()
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v2/ni");
}