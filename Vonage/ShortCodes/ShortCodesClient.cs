using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.ShortCodes
{
    public class ShortCodesClient : IShortCodesClient
    {
        public Credentials Credentials { get; set; }

        public ShortCodesClient(Credentials credentials = null)
        {
            Credentials = credentials;
        }

        public Task<OptInSearchResponse> QueryOptInsAsync(OptInQueryRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<OptInSearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/opt-in/query/json"),
                AuthType.Query,
                request,
                creds ?? Credentials);
        }

        public Task<OptInRecord> ManageOptInAsync(OptInManageRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<OptInRecord>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/opt-in/manage/json"),
                AuthType.Query,
                request,
                creds ?? Credentials);
        }

        public Task<AlertResponse> SendAlertAsync(AlertRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<AlertResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/json"),
                AuthType.Query,
                request,
                creds ?? Credentials);
        }

        public Task<TwoFactorAuthResponse> SendTwoFactorAuthAsync(TwoFactorAuthRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<TwoFactorAuthResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/2fa/json"),
                AuthType.Query,
                request,
                creds ?? Credentials);
        }

        public OptInSearchResponse QueryOptIns(OptInQueryRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<OptInSearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/opt-in/query/json"),
                AuthType.Query,
                request,
                creds ?? Credentials);
        }

        public OptInRecord ManageOptIn(OptInManageRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<OptInRecord>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/opt-in/manage/json"),
                AuthType.Query,
                request,
                creds ?? Credentials);
        }

        public AlertResponse SendAlert(AlertRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<AlertResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/json"),
                AuthType.Query,
                request,
                creds ?? Credentials);
        }

        public TwoFactorAuthResponse SendTwoFactorAuth(TwoFactorAuthRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<TwoFactorAuthResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/2fa/json"),
                AuthType.Query,
                request,
                creds ?? Credentials);
        }
    }
}