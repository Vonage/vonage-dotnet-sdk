#region
using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Common.Extensions;
using Vonage.Test.SubAccounts;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.IdentityInsights.GetInsights;

[Trait("Category", "E2E")]
public class E2ETest() : E2EBase(typeof(E2ETest).Namespace)
{
    [Fact]
    public async Task GetInsights()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/identity-insights/v1/requests")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.IdentityInsightsClient.GetInsightsAsync(RequestBuilderTest.BuildRequest())
            .Should()
            .BeSuccessAsync(SerializationTest.GetExpectedInsights());
    }

    [Fact]
    public async Task GetInsightsWithDefaultValues()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/identity-insights/v1/requests")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerializeWithDefaultValues)))
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.IdentityInsightsClient
            .GetInsightsAsync(RequestBuilderTest.BuildRequestWithDefaultValues())
            .Should()
            .BeSuccessAsync(SerializationTest.GetExpectedInsights());
    }
}