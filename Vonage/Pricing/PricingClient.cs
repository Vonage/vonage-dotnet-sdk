using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Pricing;

public class PricingClient : IPricingClient
{
    private readonly Configuration configuration;
    public Credentials Credentials { get; set; }

    public PricingClient(Credentials creds = null)
    {
        this.Credentials = creds;
        this.configuration = Configuration.Instance;
    }

    internal PricingClient(Credentials credentials, Configuration configuration)
    {
        this.Credentials = credentials;
        this.configuration = configuration;
    }

    public PricingResult RetrievePrefixPricing(string type, PricingPrefixRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoGetRequestWithQueryParameters<PricingResult>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-prefix-pricing/outbound/{type}"),
            AuthType.Query,
            request
        );

    public Task<PricingResult> RetrievePrefixPricingAsync(string type, PricingPrefixRequest request,
        Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoGetRequestWithQueryParametersAsync<PricingResult>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-prefix-pricing/outbound/{type}"),
            AuthType.Query,
            request
        );

    public PricingResult RetrievePricingAllCountries(string type, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoGetRequestWithQueryParameters<PricingResult>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
            AuthType.Query
        );

    public Task<PricingResult> RetrievePricingAllCountriesAsync(string type, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoGetRequestWithQueryParametersAsync<PricingResult>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
            AuthType.Query
        );

    public Country RetrievePricingCountry(string type, PricingCountryRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoGetRequestWithQueryParameters<Country>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
            AuthType.Query,
            request
        );

    public Task<Country> RetrievePricingCountryAsync(string type, PricingCountryRequest request,
        Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoGetRequestWithQueryParametersAsync<Country>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
            AuthType.Query,
            request
        );
}