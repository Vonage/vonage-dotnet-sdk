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

    /// <inheritdoc/>
    public PricingResult RetrievePrefixPricing(string type, PricingPrefixRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration).DoGetRequestWithQueryParameters<PricingResult>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-prefix-pricing/outbound/{type}"),
            AuthType.Query,
            request
        );

    /// <inheritdoc/>
    public Task<PricingResult> RetrievePrefixPricingAsync(string type, PricingPrefixRequest request,
        Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoGetRequestWithQueryParametersAsync<PricingResult>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-prefix-pricing/outbound/{type}"),
                AuthType.Query,
                request
            );

    /// <inheritdoc/>
    public PricingResult RetrievePricingAllCountries(string type, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration).DoGetRequestWithQueryParameters<PricingResult>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
            AuthType.Query
        );

    /// <inheritdoc/>
    public Task<PricingResult> RetrievePricingAllCountriesAsync(string type, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoGetRequestWithQueryParametersAsync<PricingResult>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
                AuthType.Query
            );

    /// <inheritdoc/>
    public Country RetrievePricingCountry(string type, PricingCountryRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration).DoGetRequestWithQueryParameters<Country>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
            AuthType.Query,
            request
        );

    /// <inheritdoc/>
    public Task<Country> RetrievePricingCountryAsync(string type, PricingCountryRequest request,
        Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration).DoGetRequestWithQueryParametersAsync<Country>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
            AuthType.Query,
            request
        );

    private Credentials GetCredentials(Credentials overridenCredentials) => overridenCredentials ?? this.Credentials;
}