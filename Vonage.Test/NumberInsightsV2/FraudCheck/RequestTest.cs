#region
using Vonage.NumberInsightV2.FraudCheck;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.NumberInsightsV2.FraudCheck;

[Trait("Category", "Request")]
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
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v2/ni");
}