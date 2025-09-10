#region
using Vonage.Pricing;
#endregion

namespace Vonage.Test.Pricing;

internal static class PricingTestsTestData
{
    internal static PricingCountryRequest CreateCountryRequest() =>
        new PricingCountryRequest {Country = "CA"};

    internal static PricingPrefixRequest CreatePrefixRequest() =>
        new PricingPrefixRequest {Prefix = "1"};
}