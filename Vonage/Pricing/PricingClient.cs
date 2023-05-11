using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Pricing;

public class PricingClient : IPricingClient
{
    public Credentials Credentials { get; set; }

    public PricingClient(Credentials creds = null) => this.Credentials = creds;

    public PricingResult RetrievePrefixPricing(string type, PricingPrefixRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials).DoGetRequestWithQueryParameters<PricingResult>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-prefix-pricing/outbound/{type}"),
            AuthType.Query,
            request
        );

    public Task<PricingResult> RetrievePrefixPricingAsync(string type, PricingPrefixRequest request,
        Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials).DoGetRequestWithQueryParametersAsync<PricingResult>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-prefix-pricing/outbound/{type}"),
            AuthType.Query,
            request
        );

    public PricingResult RetrievePricingAllCountries(string type, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials).DoGetRequestWithQueryParameters<PricingResult>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
            AuthType.Query
        );

    public Task<PricingResult> RetrievePricingAllCountriesAsync(string type, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials).DoGetRequestWithQueryParametersAsync<PricingResult>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
            AuthType.Query
        );

    public Country RetrievePricingCountry(string type, PricingCountryRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials).DoGetRequestWithQueryParameters<Country>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
            AuthType.Query,
            request
        );

    public Task<Country> RetrievePricingCountryAsync(string type, PricingCountryRequest request,
        Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials).DoGetRequestWithQueryParametersAsync<Country>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
            AuthType.Query,
            request
        );
}