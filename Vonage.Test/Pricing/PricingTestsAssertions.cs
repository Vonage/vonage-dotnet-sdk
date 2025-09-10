#region
using FluentAssertions;
using Vonage.Pricing;
#endregion

namespace Vonage.Test.Pricing;

internal static class PricingTestsAssertions
{
    private static void AssertNetworkProperties(Network network)
    {
        network.NetworkCode.Should().Be("302530");
        network.NetworkName.Should().Be("Keewaytinook Okimakanak");
        network.Mnc.Should().Be("530");
        network.Mcc.Should().Be("302");
        network.Currency.Should().Be("EUR");
        network.Price.Should().Be("0.00590000");
        network.Type.Should().Be("mobile");
    }

    internal static void ShouldMatchExpectedPricingResult(this PricingResult actual)
    {
        actual.Count.Should().Be("243");
        actual.Countries[0].CountryName.Should().Be("Canada");
        actual.Countries[0].CountryDisplayName.Should().Be("Canada");
        actual.Countries[0].Currency.Should().Be("EUR");
        actual.Countries[0].DefaultPrice.Should().Be("0.00620000");
        actual.Countries[0].DialingPrefix.Should().Be("1");
        AssertNetworkProperties(actual.Countries[0].Networks[0]);
    }

    internal static void ShouldMatchExpectedCountry(this Country actual)
    {
        actual.CountryCode.Should().Be("CA");
        actual.CountryName.Should().Be("Canada");
        actual.CountryDisplayName.Should().Be("Canada");
        actual.Currency.Should().Be("EUR");
        actual.DefaultPrice.Should().Be("0.00620000");
        actual.DialingPrefix.Should().Be("1");
        AssertNetworkProperties(actual.Networks[0]);
    }
}