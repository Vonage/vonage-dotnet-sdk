using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Pricing;

public class PricingClient : IPricingClient
{
    public PricingClient(Credentials creds = null)
    {
        Credentials = creds;
    }
        
    public Credentials Credentials { get; set; }
        
    public Task<Country> RetrievePricingCountryAsync(string type, PricingCountryRequest request, Credentials creds = null)
    {
        return ApiRequest.DoGetRequestWithQueryParametersAsync<Country>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
            ApiRequest.AuthType.Query,
            request,
            creds ?? Credentials
        );
    }

    public Task<PricingResult> RetrievePricingAllCountriesAsync(string type, Credentials creds = null)
    {
        return ApiRequest.DoGetRequestWithQueryParametersAsync<PricingResult>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
            ApiRequest.AuthType.Query,
            credentials: creds ?? Credentials
        );
    }

    public Task<PricingResult> RetrievePrefixPricingAsync(string type, PricingPrefixRequest request, Credentials creds = null)
    {
        return ApiRequest.DoGetRequestWithQueryParametersAsync<PricingResult>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-prefix-pricing/outbound/{type}"),
            ApiRequest.AuthType.Query,
            request,
            creds ?? Credentials
        );
    }

    public Country RetrievePricingCountry(string type, PricingCountryRequest request, Credentials creds = null)
    {
        return ApiRequest.DoGetRequestWithQueryParameters<Country>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
            ApiRequest.AuthType.Query,
            request,
            creds ?? Credentials
        );
    }

    public PricingResult RetrievePricingAllCountries(string type, Credentials creds = null)
    {
        return ApiRequest.DoGetRequestWithQueryParameters<PricingResult>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
            ApiRequest.AuthType.Query,
            credentials: creds ?? Credentials
        );
    }

    public PricingResult RetrievePrefixPricing(string type, PricingPrefixRequest request, Credentials creds = null)
    {
        return ApiRequest.DoGetRequestWithQueryParameters<PricingResult>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-prefix-pricing/outbound/{type}"),
            ApiRequest.AuthType.Query,
            request,
            creds ?? Credentials
        );
    }
}