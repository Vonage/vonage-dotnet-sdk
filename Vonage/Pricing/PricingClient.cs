using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Pricing
{
    public class PricingClient : IPricingClient
    {
        public PricingClient(Credentials creds = null, int? timeout = null)
        {
            Credentials = creds;
            Timeout = timeout;
        }
        
        public Credentials Credentials { get; set; }
        public int? Timeout { get; private set; }

        public Task<Country> RetrievePricingCountryAsync(string type, PricingCountryRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<Country>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials,
                timeout: Timeout
            );
        }

        public Task<PricingResult> RetrievePricingAllCountriesAsync(string type, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<PricingResult>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
                ApiRequest.AuthType.Query,
                credentials: creds ?? Credentials,
                timeout: Timeout
            );
        }

        public Task<PricingResult> RetrievePrefixPricingAsync(string type, PricingPrefixRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<PricingResult>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-prefix-pricing/outbound/{type}"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials,
                timeout: Timeout
            );
        }

        public Country RetrievePricingCountry(string type, PricingCountryRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<Country>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials,
                timeout: Timeout
            );
        }

        public PricingResult RetrievePricingAllCountries(string type, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<PricingResult>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
                ApiRequest.AuthType.Query,
                credentials: creds ?? Credentials,
                timeout: Timeout
            );
        }

        public PricingResult RetrievePrefixPricing(string type, PricingPrefixRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<PricingResult>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-prefix-pricing/outbound/{type}"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials,
                timeout: Timeout
            );
        }
    }
}