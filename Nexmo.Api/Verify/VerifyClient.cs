using Nexmo.Api.Request;

namespace Nexmo.Api.Verify
{
    public class VerifyClient : IVerifyClient
    {
        public Credentials Credentials { get; set; }

        public VerifyClient(Credentials creds)
        {
            Credentials = creds;
        }
        public VerifyResponse VerifyRequest(VerifyRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithUrlContent<VerifyResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials
            );
        }

        public VerifyCheckResponse VerifyCheck(VerifyCheckRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithUrlContent<VerifyCheckResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/check/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials
            );
        }

        public VerifySearchResponse VerifySearch(VerifySearchRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithUrlContent<VerifySearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/search/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials
            );
        }

        public VerifyControlResponse VerifyControl(VerifyControlRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithUrlContent<VerifyControlResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/control/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials
            );
        }
    }
}