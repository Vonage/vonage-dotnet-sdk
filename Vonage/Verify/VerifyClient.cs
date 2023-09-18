using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Verify;

public class VerifyClient : IVerifyClient
{
    public Credentials Credentials { get; set; }

    public VerifyClient(Credentials creds = null) => this.Credentials = creds;

    public void ValidateVerifyResponse(VerifyResponseBase response)
    {
        if (response.Status != "0")
        {
            throw new VonageVerifyResponseException(
                    $"Verify Request Failed with status: {response.Status} and Error Text: {response.ErrorText}")
            { Response = response };
        }
    }

    public VerifyCheckResponse VerifyCheck(VerifyCheckRequest request, Credentials creds = null)
    {
        var response = new ApiRequest(creds ?? this.Credentials).DoPostRequestUrlContentFromObject<VerifyCheckResponse>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/check/json"),
            request
        );
        this.ValidateVerifyResponse(response);
        return response;
    }

    public async Task<VerifyCheckResponse> VerifyCheckAsync(VerifyCheckRequest request, Credentials creds = null)
    {
        var response = await new ApiRequest(creds ?? this.Credentials)
            .DoPostRequestUrlContentFromObjectAsync<VerifyCheckResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/check/json"),
                request
            );
        this.ValidateVerifyResponse(response);
        return response;
    }

    public VerifyControlResponse VerifyControl(VerifyControlRequest request, Credentials creds = null)
    {
        var response = new ApiRequest(creds ?? this.Credentials)
            .DoPostRequestUrlContentFromObject<VerifyControlResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/control/json"),
                request
            );
        this.ValidateVerifyResponse(response);
        return response;
    }

    public async Task<VerifyControlResponse> VerifyControlAsync(VerifyControlRequest request, Credentials creds = null)
    {
        var response = await new ApiRequest(creds ?? this.Credentials)
            .DoPostRequestUrlContentFromObjectAsync<VerifyControlResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/control/json"),
                request
            );
        this.ValidateVerifyResponse(response);
        return response;
    }

    public VerifyResponse VerifyRequest(VerifyRequest request, Credentials creds = null)
    {
        var response = new ApiRequest(creds ?? this.Credentials).DoPostRequestUrlContentFromObject<VerifyResponse>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/json"),
            request
        );
        this.ValidateVerifyResponse(response);
        return response;
    }

    public async Task<VerifyResponse> VerifyRequestAsync(VerifyRequest request, Credentials creds = null)
    {
        var response = await new ApiRequest(creds ?? this.Credentials)
            .DoPostRequestUrlContentFromObjectAsync<VerifyResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/json"),
                request
            );
        this.ValidateVerifyResponse(response);
        return response;
    }

    public VerifyResponse VerifyRequestWithPSD2(Psd2Request request, Credentials creds = null)
    {
        var response = new ApiRequest(creds ?? this.Credentials).DoPostRequestUrlContentFromObject<VerifyResponse>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/psd2/json"),
            request
        );
        this.ValidateVerifyResponse(response);
        return response;
    }

    public async Task<VerifyResponse> VerifyRequestWithPSD2Async(Psd2Request request, Credentials creds = null)
    {
        var response = await new ApiRequest(creds ?? this.Credentials)
            .DoPostRequestUrlContentFromObjectAsync<VerifyResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/psd2/json"),
                request
            );
        this.ValidateVerifyResponse(response);
        return response;
    }

    public VerifySearchResponse VerifySearch(VerifySearchRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials).DoGetRequestWithQueryParameters<VerifySearchResponse>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/search/json"),
            AuthType.Query,
            request
        );

    public Task<VerifySearchResponse> VerifySearchAsync(VerifySearchRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials).DoGetRequestWithQueryParametersAsync<VerifySearchResponse>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/search/json"),
            AuthType.Query,
            request
        );
}