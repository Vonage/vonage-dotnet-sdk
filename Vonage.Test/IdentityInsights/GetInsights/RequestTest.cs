#region
using Vonage.IdentityInsights.GetInsights;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.IdentityInsights.GetInsights;

[Trait("Category", "Request")]
[Trait("Product", "IdentityInsights")]
public class RequestTest
{
    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        GetInsightsRequest
            .Build()
            .WithPhoneNumber(RequestBuilderTest.ValidPhoneNumber)
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/identity-insights/v1/requests");
}