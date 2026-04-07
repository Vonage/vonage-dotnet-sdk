#region
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Request;
#endregion

namespace Vonage.ShortCodes;

/// <summary>
///     Represents a client for the Vonage US Short Codes API, enabling sending of event-based alerts and two-factor
///     authentication messages via pre-approved short codes.
/// </summary>
public class ShortCodesClient : IShortCodesClient
{
    private readonly Configuration configuration;
    private readonly ITimeProvider timeProvider = new TimeProvider();

    /// <summary>
    ///     Initializes a new instance of the <see cref="ShortCodesClient"/> class.
    /// </summary>
    /// <param name="credentials">Optional credentials to use for authentication.</param>
    public ShortCodesClient(Credentials credentials = null)
    {
        this.Credentials = credentials;
        this.configuration = Configuration.Instance;
    }

    internal ShortCodesClient(Credentials credentials, Configuration configuration, ITimeProvider timeProvider)
    {
        this.Credentials = credentials;
        this.configuration = configuration;
        this.timeProvider = timeProvider;
    }

    /// <summary>
    ///     Gets or sets the credentials used for authenticating API requests.
    /// </summary>
    public Credentials Credentials { get; set; }

    /// <inheritdoc/>
    public Task<OptInRecord> ManageOptInAsync(OptInManageRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<OptInRecord>(
                this.configuration.BuildUri(ApiRequest.UriType.Rest, "/sc/us/alert/opt-in/manage/json"),
                AuthType.Basic,
                request);

    /// <inheritdoc/>
    public Task<OptInSearchResponse> QueryOptInsAsync(OptInQueryRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<OptInSearchResponse>(
                this.configuration.BuildUri(ApiRequest.UriType.Rest, "/sc/us/alert/opt-in/query/json"),
                AuthType.Basic,
                request);

    /// <inheritdoc/>
    public Task<AlertResponse> SendAlertAsync(AlertRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<AlertResponse>(
                this.configuration.BuildUri(ApiRequest.UriType.Rest, "/sc/us/alert/json"),
                AuthType.Basic,
                request);

    /// <inheritdoc/>
    public Task<TwoFactorAuthResponse> SendTwoFactorAuthAsync(TwoFactorAuthRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<TwoFactorAuthResponse>(
                this.configuration.BuildUri(ApiRequest.UriType.Rest, "/sc/us/2fa/json"),
                AuthType.Basic,
                request);

    private Credentials GetCredentials(Credentials overridenCredentials) => overridenCredentials ?? this.Credentials;
}