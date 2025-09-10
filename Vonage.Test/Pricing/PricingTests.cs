#region
using System.Threading.Tasks;
using Vonage.Pricing;
using Vonage.Request;
using Vonage.Serialization;
using Vonage.Test.Common;
using Xunit;
#endregion

namespace Vonage.Test.Pricing;

[Trait("Category", "Legacy")]
public class PricingTests : TestBase
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(PricingTests).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    [Fact]
    public async Task GetPricingAllCountries()
    {
        this.Setup($"{this.RestUrl}/account/get-pricing/outbound/sms", this.helper.GetResponseJson());
        var response = await this.BuildPricingClient().RetrievePricingAllCountriesAsync("sms");
        response.ShouldMatchExpectedPricingResult();
    }

    [Fact]
    public async Task GetPricingForCountry()
    {
        this.Setup($"{this.RestUrl}/account/get-pricing/outbound/sms?country=CA&", this.helper.GetResponseJson());
        var response = await this.BuildPricingClient()
            .RetrievePricingCountryAsync("sms", PricingTestsTestData.CreateCountryRequest());
        response.ShouldMatchExpectedCountry();
    }

    [Fact]
    public async Task GetPricingForPrefix()
    {
        this.Setup($"{this.RestUrl}/account/get-prefix-pricing/outbound/sms?prefix=1&", this.helper.GetResponseJson());
        var response = await this.BuildPricingClient()
            .RetrievePrefixPricingAsync("sms", PricingTestsTestData.CreatePrefixRequest());
        response.ShouldMatchExpectedPricingResult();
    }

    private IPricingClient BuildPricingClient() =>
        this.BuildVonageClient(Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret)).PricingClient;
}