#region
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Request;
#endregion

namespace Vonage.Pricing;

/// <summary>
///     Provides access to the Pricing API for retrieving outbound pricing information for SMS and voice services.
/// </summary>
public class PricingClient : IPricingClient
{
    private readonly Configuration configuration;
    private readonly ITimeProvider timeProvider = new TimeProvider();

    /// <summary>
    ///     Initializes a new instance of the <see cref="PricingClient"/> class.
    /// </summary>
    /// <param name="creds">Optional credentials to use for API requests. If not provided, uses default configuration.</param>
    public PricingClient(Credentials creds = null)
    {
        this.Credentials = creds;
        this.configuration = Configuration.Instance;
    }

    internal PricingClient(Credentials credentials, Configuration configuration, ITimeProvider timeProvider)
    {
        this.Credentials = credentials;
        this.configuration = configuration;
        this.timeProvider = timeProvider;
    }

    /// <summary>
    ///     The credentials used for authenticating API requests.
    /// </summary>
    public Credentials Credentials { get; set; }

    /// <inheritdoc/>
    public Task<PricingResult> RetrievePrefixPricingAsync(string type, PricingPrefixRequest request,
        Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<PricingResult>
            (
                this.configuration.BuildUri(ApiRequest.UriType.Rest, $"/account/get-prefix-pricing/outbound/{type}"),
                AuthType.Basic,
                request
            );

    /// <inheritdoc/>
    public Task<PricingResult> RetrievePricingAllCountriesAsync(string type, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<PricingResult>
            (
                this.configuration.BuildUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
                AuthType.Basic
            );

    /// <inheritdoc/>
    public Task<Country> RetrievePricingCountryAsync(string type, PricingCountryRequest request,
        Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<Country>
            (
                this.configuration.BuildUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
                AuthType.Basic,
                request
            );

    private Credentials GetCredentials(Credentials overridenCredentials) => overridenCredentials ?? this.Credentials;
}