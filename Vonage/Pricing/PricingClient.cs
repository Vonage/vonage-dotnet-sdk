using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Pricing
{
    public class PricingClient : IPricingClient
    {
        public PricingClient(Credentials creds = null)
        {
            Credentials = creds;
        }
        
        public Credentials Credentials { get; set; }
        
        public Country RetrievePricingCountry(string type, PricingCountryRequest request, Credentials creds = null)
        {
            return RetrievePricingCountryAsync(type, request, creds).GetAwaiter().GetResult();
        }

        public PricingResult RetrievePricingAllCountries(string type, Credentials creds = null)
        {
            return RetrievePricingAllCountriesAsync(type, creds).GetAwaiter().GetResult();
        }

        public PricingResult RetrievePrefixPricing(string type, PricingPrefixRequest request, Credentials creds = null)
        {
            return RetrievePrefixPricingAsync(type, request, creds).GetAwaiter().GetResult();
        }

        public async Task<Country> RetrievePricingCountryAsync(string type, PricingCountryRequest request, Credentials creds = null)
        {
            return await ApiRequest.DoGetRequestWithQueryParametersAsync<Country>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials
            );
        }

        public async Task<PricingResult> RetrievePricingAllCountriesAsync(string type, Credentials creds = null)
        {
            return await ApiRequest.DoGetRequestWithQueryParametersAsync<PricingResult>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
                ApiRequest.AuthType.Query,
                credentials: creds ?? Credentials
            );
        }

        public async Task<PricingResult> RetrievePrefixPricingAsync(string type, PricingPrefixRequest request, Credentials creds = null)
        {
            return await ApiRequest.DoGetRequestWithQueryParametersAsync<PricingResult>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-prefix-pricing/outbound/{type}"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials
            );
        }
    }
}