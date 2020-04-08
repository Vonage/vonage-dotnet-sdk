using Nexmo.Api.Request;

namespace Nexmo.Api.Pricing
{
    public interface IPricingClient
    {
        Country RetrievePricingCountry(string type, PricingCountryRequest request, Credentials creds = null);
        PricingResult RetrievePricingAllCountries(string type, Credentials creds = null);
        PricingResult RetrievePrefixPricing(string type, PricingPrefixRequest request, Credentials creds = null);
    }
}