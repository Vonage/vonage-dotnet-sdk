#region
using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Vonage.Pricing;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.TestHelpers;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Pricing;

[Trait("Category", "Legacy")]
public class PricingTests : IDisposable
{
    private readonly TestingContext context = TestingContext.WithBasicCredentials();

    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(PricingTests).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    public void Dispose()
    {
        this.context?.Dispose();
        GC.SuppressFinalize(this);
    }

    private IResponseBuilder RespondWithSuccess([CallerMemberName] string testName = null) =>
        Response.Create()
            .WithStatusCode(HttpStatusCode.OK)
            .WithBody(this.helper.GetResponseJson(testName));

    private IPricingClient BuildPricingClient() => this.context.VonageClient.PricingClient;

    [Fact]
    public async Task GetPricingAllCountries()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/account/get-pricing/outbound/sms")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildPricingClient().RetrievePricingAllCountriesAsync("sms");
        response.ShouldMatchExpectedPricingResult();
    }

    [Fact]
    public async Task GetPricingForCountry()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/account/get-pricing/outbound/sms")
                .WithParam("country", "CA")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildPricingClient()
            .RetrievePricingCountryAsync("sms", PricingTestsTestData.CreateCountryRequest());
        response.ShouldMatchExpectedCountry();
    }

    [Fact]
    public async Task GetPricingForPrefix()
    {
        this.context.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/account/get-prefix-pricing/outbound/sms")
                .WithParam("prefix", "1")
                .WithHeader("Authorization", this.context.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(this.RespondWithSuccess());
        var response = await this.BuildPricingClient()
            .RetrievePrefixPricingAsync("sms", PricingTestsTestData.CreatePrefixRequest());
        response.ShouldMatchExpectedPricingResult();
    }
}