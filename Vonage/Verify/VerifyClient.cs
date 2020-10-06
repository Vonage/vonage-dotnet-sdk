using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Verify
{
    public class VerifyClient : IVerifyClient
    {
        public Credentials Credentials { get; set; }

        public VerifyClient(Credentials creds = null)
        {
            Credentials = creds;
        }
        public async Task<VerifyResponse> VerifyRequestAsync(VerifyRequest request, Credentials creds = null)
        {
            var response = await ApiRequest.DoPostRequestUrlContentFromObjectAsync<VerifyResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/json"),
                request,
                creds ?? Credentials
            );
            ValidateVerifyResponse(response);
            return response;
        }

        public async Task<VerifyCheckResponse> VerifyCheckAsync(VerifyCheckRequest request, Credentials creds = null)
        {
            var response = await ApiRequest.DoPostRequestUrlContentFromObjectAsync<VerifyCheckResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/check/json"),
                request,
                creds ?? Credentials
            );
            ValidateVerifyResponse(response);
            return response;
        }

        public Task<VerifySearchResponse> VerifySearchAsync(VerifySearchRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<VerifySearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/search/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials
            );
        }

        public async Task<VerifyControlResponse> VerifyControlAsync(VerifyControlRequest request, Credentials creds = null)
        {
            var response = await ApiRequest.DoPostRequestUrlContentFromObjectAsync<VerifyControlResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/control/json"),
                request,
                creds ?? Credentials
            );
            ValidateVerifyResponse(response);
            return response;
        }

        public async Task<VerifyResponse> VerifyRequestWithPSD2Async(Psd2Request request, Credentials creds)
        {
            var response = await ApiRequest.DoPostRequestUrlContentFromObjectAsync<VerifyResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/psd2/json"),
                request,
                creds ?? Credentials
            );
            ValidateVerifyResponse(response);
            return response;
        }

        public void ValidateVerifyResponse(VerifyResponseBase response)
        {
            if (response.Status != "0")
            {
                throw new VonageVerifyResponseException($"Verify Request Failed with status: {response.Status} and Error Text: {response.ErrorText}") { Response = response };
            }
        }
    }
}