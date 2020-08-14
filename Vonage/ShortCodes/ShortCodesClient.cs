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
            return ApiRequest.DoGetRequestWithQueryParameters<OptInSearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/opt-in/query/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials);
        }

        public OptInRecord ManageOptIn(OptInManageRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<OptInRecord>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/opt-in/manage/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials);
        }

        public AlertResponse SendAlert(AlertRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<AlertResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/alert/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials);
        }

        public TwoFactorAuthResponse SendTwoFactorAuth(TwoFactorAuthRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<TwoFactorAuthResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sc/us/2fa/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials);
        }
    }
}