using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.ShortCodes;

public class ShortCodesClient : IShortCodesClient
{
    public Credentials Credentials { get; set; }

    public ShortCodesClient(Credentials credentials = null)
    {
        this.Credentials = credentials;
    }

    public OptInRecord ManageOptIn(OptInManageRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials).DoGetRequestWithQueryParameters<OptInRecord>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/opt-in/manage/json"),
            AuthType.Query,
            request);

    public Task<OptInRecord> ManageOptInAsync(OptInManageRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials).DoGetRequestWithQueryParametersAsync<OptInRecord>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/opt-in/manage/json"),
            AuthType.Query,
            request);

    public OptInSearchResponse QueryOptIns(OptInQueryRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials).DoGetRequestWithQueryParameters<OptInSearchResponse>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/opt-in/query/json"),
            AuthType.Query,
            request);

    public Task<OptInSearchResponse> QueryOptInsAsync(OptInQueryRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials).DoGetRequestWithQueryParametersAsync<OptInSearchResponse>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/opt-in/query/json"),
            AuthType.Query,
            request);

    public AlertResponse SendAlert(AlertRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials).DoGetRequestWithQueryParameters<AlertResponse>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/json"),
            AuthType.Query,
            request);

    public Task<AlertResponse> SendAlertAsync(AlertRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials).DoGetRequestWithQueryParametersAsync<AlertResponse>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/json"),
            AuthType.Query,
            request);

    public TwoFactorAuthResponse SendTwoFactorAuth(TwoFactorAuthRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials).DoGetRequestWithQueryParameters<TwoFactorAuthResponse>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/2fa/json"),
            AuthType.Query,
            request);

    public Task<TwoFactorAuthResponse> SendTwoFactorAuthAsync(TwoFactorAuthRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials).DoGetRequestWithQueryParametersAsync<TwoFactorAuthResponse>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/2fa/json"),
            AuthType.Query,
            request);
}