using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.ShortCodes
{
    public class ShortCodesClient : IShortCodesClient
    {
        public Credentials Credentials { get; set; }
        public int? Timeout { get; private set; }

        public ShortCodesClient(Credentials credentials = null, int? timeout = null)
        {
            Credentials = credentials;
            Timeout = timeout;
        }

        public Task<OptInSearchResponse> QueryOptInsAsync(OptInQueryRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<OptInSearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/opt-in/query/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials,
                timeout: Timeout);
        }

        public Task<OptInRecord> ManageOptInAsync(OptInManageRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<OptInRecord>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/opt-in/manage/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials,
                timeout: Timeout);
        }

        public Task<AlertResponse> SendAlertAsync(AlertRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<AlertResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials,
                timeout: Timeout);
        }

        public Task<TwoFactorAuthResponse> SendTwoFactorAuthAsync(TwoFactorAuthRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<TwoFactorAuthResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/2fa/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials,
                timeout: Timeout);
        }

        public OptInSearchResponse QueryOptIns(OptInQueryRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<OptInSearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/opt-in/query/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials,
                timeout: Timeout);
        }

        public OptInRecord ManageOptIn(OptInManageRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<OptInRecord>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/opt-in/manage/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials,
                timeout: Timeout);
        }

        public AlertResponse SendAlert(AlertRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<AlertResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials,
                timeout: Timeout);
        }

        public TwoFactorAuthResponse SendTwoFactorAuth(TwoFactorAuthRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<TwoFactorAuthResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/2fa/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials,
                timeout: Timeout);
        }
    }
}