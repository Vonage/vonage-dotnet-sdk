#region
using System.Net;
using System.Threading.Tasks;
using Vonage.IdentityInsights;
using Vonage.Test.Common.Extensions;
using Vonage.Test.SubAccounts;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.IdentityInsights.GetInsights;

[Trait("Category", "E2E")]
[Trait("Product", "IdentityInsights")]
public class E2ETest() : E2EBase(typeof(E2ETest).Namespace)
{
    private const string Endpoint = "/identity-insights/v1/requests";

    public static TheoryData<RegionTestSetup> GetSetups =>
    [
        new RegionTestSetup(VonageUrls.Region.US, string.Concat("/AMER", Endpoint)),
        new RegionTestSetup(VonageUrls.Region.EU, string.Concat("/EMEA", Endpoint)),
    ];

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task GetInsights(RegionTestSetup setup)
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath(setup.BaseUri)
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.SetupClient(setup.Region).GetInsightsAsync(RequestBuilderTest.BuildRequest())
            .Should()
            .BeSuccessAsync(SerializationTest.GetExpectedInsights());
    }

    [Theory]
    [MemberData(nameof(GetSetups))]
    public async Task GetInsightsWithDefaultValues(RegionTestSetup setup)
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath(setup.BaseUri)
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerializeWithDefaultValues)))
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.SetupClient(setup.Region).GetInsightsAsync(RequestBuilderTest.BuildRequestWithDefaultValues())
            .Should()
            .BeSuccessAsync(SerializationTest.GetExpectedInsights());
    }

    private IIdentityInsightsClient SetupClient(VonageUrls.Region region) =>
        region == VonageUrls.Region.EU
            ? this.Helper.VonageClient.IdentityInsightsClient.WithEuRegion()
            : this.Helper.VonageClient.IdentityInsightsClient;

    public record RegionTestSetup(VonageUrls.Region Region, string BaseUri);
}