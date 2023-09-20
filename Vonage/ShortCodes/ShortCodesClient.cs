using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.ShortCodes;

public class ShortCodesClient : IShortCodesClient
{
    private readonly Configuration configuration;
    public Credentials Credentials { get; set; }

    public ShortCodesClient(Credentials credentials = null)
    {
        this.Credentials = credentials;
        this.configuration = Configuration.Instance;
    }

    internal ShortCodesClient(Credentials credentials, Configuration configuration)
    {
        this.Credentials = credentials;
        this.configuration = configuration;
    }
    
    private Credentials GetCredentials(Credentials overridenCredentials) => overridenCredentials ?? this.Credentials;

    /// <inheritdoc/>
    public OptInRecord ManageOptIn(OptInManageRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration).DoGetRequestWithQueryParameters<OptInRecord>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/opt-in/manage/json"),
            AuthType.Query,
            request);

    /// <inheritdoc/>
    public Task<OptInRecord> ManageOptInAsync(OptInManageRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration).DoGetRequestWithQueryParametersAsync<OptInRecord>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/opt-in/manage/json"),
            AuthType.Query,
            request);

    /// <inheritdoc/>
    public OptInSearchResponse QueryOptIns(OptInQueryRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration).DoGetRequestWithQueryParameters<OptInSearchResponse>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/opt-in/query/json"),
            AuthType.Query,
            request);

    /// <inheritdoc/>
    public Task<OptInSearchResponse> QueryOptInsAsync(OptInQueryRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration).DoGetRequestWithQueryParametersAsync<OptInSearchResponse>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/opt-in/query/json"),
            AuthType.Query,
            request);

    /// <inheritdoc/>
    public AlertResponse SendAlert(AlertRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration).DoGetRequestWithQueryParameters<AlertResponse>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/json"),
            AuthType.Query,
            request);

    /// <inheritdoc/>
    public Task<AlertResponse> SendAlertAsync(AlertRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration).DoGetRequestWithQueryParametersAsync<AlertResponse>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/json"),
            AuthType.Query,
            request);

    /// <inheritdoc/>
    public TwoFactorAuthResponse SendTwoFactorAuth(TwoFactorAuthRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration).DoGetRequestWithQueryParameters<TwoFactorAuthResponse>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/2fa/json"),
            AuthType.Query,
            request);

    /// <inheritdoc/>
    public Task<TwoFactorAuthResponse> SendTwoFactorAuthAsync(TwoFactorAuthRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration).DoGetRequestWithQueryParametersAsync<TwoFactorAuthResponse>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/2fa/json"),
            AuthType.Query,
            request);
}