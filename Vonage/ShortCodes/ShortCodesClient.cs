using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Request;

namespace Vonage.ShortCodes;

public class ShortCodesClient : IShortCodesClient
{
    private readonly Configuration configuration;
    private readonly ITimeProvider timeProvider = new TimeProvider();

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

    public Credentials Credentials { get; set; }

    /// <inheritdoc/>
    public Task<OptInRecord> ManageOptInAsync(OptInManageRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<OptInRecord>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, this.configuration, "/sc/us/alert/opt-in/manage/json"),
                AuthType.Query,
                request);

    /// <inheritdoc/>
    public Task<OptInSearchResponse> QueryOptInsAsync(OptInQueryRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<OptInSearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, this.configuration, "/sc/us/alert/opt-in/query/json"),
                AuthType.Query,
                request);

    /// <inheritdoc/>
    public Task<AlertResponse> SendAlertAsync(AlertRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<AlertResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, this.configuration, "/sc/us/alert/json"),
                AuthType.Query,
                request);

    /// <inheritdoc/>
    public Task<TwoFactorAuthResponse> SendTwoFactorAuthAsync(TwoFactorAuthRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<TwoFactorAuthResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, this.configuration, "/sc/us/2fa/json"),
                AuthType.Query,
                request);

    private Credentials GetCredentials(Credentials overridenCredentials) => overridenCredentials ?? this.Credentials;
}