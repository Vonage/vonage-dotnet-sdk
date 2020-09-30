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
        public OptInSearchResponse QueryOptIns(OptInQueryRequest request, Credentials creds = null)
        {
            return QueryOptInsAsync(request, creds).GetAwaiter().GetResult();
        }

        public OptInRecord ManageOptIn(OptInManageRequest request, Credentials creds = null)
        {
            return ManageOptInAsync(request, creds).GetAwaiter().GetResult();
        }

        public AlertResponse SendAlert(AlertRequest request, Credentials creds = null)
        {
            return SendAlertAsync(request, creds).GetAwaiter().GetResult();
        }

        public TwoFactorAuthResponse SendTwoFactorAuth(TwoFactorAuthRequest request, Credentials creds = null)
        {
            return SendTwoFactorAuthAsync(request, creds).GetAwaiter().GetResult();
        }

        public async Task<OptInSearchResponse> QueryOptInsAsync(OptInQueryRequest request, Credentials creds = null)
        {
            return await ApiRequest.DoGetRequestWithQueryParametersAsync<OptInSearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/opt-in/query/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials);
        }

        public async Task<OptInRecord> ManageOptInAsync(OptInManageRequest request, Credentials creds = null)
        {
            return await ApiRequest.DoGetRequestWithQueryParametersAsync<OptInRecord>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/opt-in/manage/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials);
        }

        public async Task<AlertResponse> SendAlertAsync(AlertRequest request, Credentials creds = null)
        {
            return await ApiRequest.DoGetRequestWithQueryParametersAsync<AlertResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials);
        }

        public async Task<TwoFactorAuthResponse> SendTwoFactorAuthAsync(TwoFactorAuthRequest request, Credentials creds = null)
        {
            return await ApiRequest.DoGetRequestWithQueryParametersAsync<TwoFactorAuthResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/2fa/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials);
        }
    }
}