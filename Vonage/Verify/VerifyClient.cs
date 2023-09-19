using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Verify;

public class VerifyClient : IVerifyClient
{
    private readonly Configuration configuration;
    public Credentials Credentials { get; set; }

    public VerifyClient(Credentials creds = null)
    {
        this.Credentials = creds;
        this.configuration = Configuration.Instance;
    }

    internal VerifyClient(Credentials credentials, Configuration configuration)
    {
        this.Credentials = credentials;
        this.configuration = configuration;
    }

    public void ValidateVerifyResponse(VerifyResponseBase response)
    {
        if (response.Status != "0")
        {
            throw new VonageVerifyResponseException(
                    $"Verify Request Failed with status: {response.Status} and Error Text: {response.ErrorText}")
                {Response = response};
        }
    }

    public VerifyCheckResponse VerifyCheck(VerifyCheckRequest request, Credentials creds = null)
    {
        var response = ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoPostRequestUrlContentFromObject<VerifyCheckResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/check/json"),
                request
            );
        this.ValidateVerifyResponse(response);
        return response;
    }

    public async Task<VerifyCheckResponse> VerifyCheckAsync(VerifyCheckRequest request, Credentials creds = null)
    {
        var response = await ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoPostRequestUrlContentFromObjectAsync<VerifyCheckResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/check/json"),
                request
            );
        this.ValidateVerifyResponse(response);
        return response;
    }

    public VerifyControlResponse VerifyControl(VerifyControlRequest request, Credentials creds = null)
    {
        var response = ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoPostRequestUrlContentFromObject<VerifyControlResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/control/json"),
                request
            );
        this.ValidateVerifyResponse(response);
        return response;
    }

    public async Task<VerifyControlResponse> VerifyControlAsync(VerifyControlRequest request, Credentials creds = null)
    {
        var response = await ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoPostRequestUrlContentFromObjectAsync<VerifyControlResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/control/json"),
                request
            );
        this.ValidateVerifyResponse(response);
        return response;
    }

    public VerifyResponse VerifyRequest(VerifyRequest request, Credentials creds = null)
    {
        var response = ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoPostRequestUrlContentFromObject<VerifyResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/json"),
                request
            );
        this.ValidateVerifyResponse(response);
        return response;
    }

    public async Task<VerifyResponse> VerifyRequestAsync(VerifyRequest request, Credentials creds = null)
    {
        var response = await ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoPostRequestUrlContentFromObjectAsync<VerifyResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/json"),
                request
            );
        this.ValidateVerifyResponse(response);
        return response;
    }

    public VerifyResponse VerifyRequestWithPSD2(Psd2Request request, Credentials creds = null)
    {
        var response = ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoPostRequestUrlContentFromObject<VerifyResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/psd2/json"),
                request
            );
        this.ValidateVerifyResponse(response);
        return response;
    }

    public async Task<VerifyResponse> VerifyRequestWithPSD2Async(Psd2Request request, Credentials creds = null)
    {
        var response = await ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoPostRequestUrlContentFromObjectAsync<VerifyResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/psd2/json"),
                request
            );
        this.ValidateVerifyResponse(response);
        return response;
    }

    public VerifySearchResponse VerifySearch(VerifySearchRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoGetRequestWithQueryParameters<VerifySearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/search/json"),
                AuthType.Query,
                request
            );

    public Task<VerifySearchResponse> VerifySearchAsync(VerifySearchRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoGetRequestWithQueryParametersAsync<VerifySearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/verify/search/json"),
                AuthType.Query,
                request
            );

    private Credentials GetCredentials(Credentials overridenCredentials) => overridenCredentials ?? this.Credentials;
}