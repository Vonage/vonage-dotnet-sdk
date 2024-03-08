using System;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Request;

namespace Vonage.Verify;

public class VerifyClient : IVerifyClient
{
    private readonly Configuration configuration;
    private readonly ITimeProvider timeProvider = new TimeProvider();

    public VerifyClient(Credentials creds = null)
    {
        this.Credentials = creds;
        this.configuration = Configuration.Instance;
    }

    internal VerifyClient(Credentials credentials, Configuration configuration, ITimeProvider timeProvider)
    {
        this.Credentials = credentials;
        this.configuration = configuration;
        this.timeProvider = timeProvider;
    }

    public Credentials Credentials { get; set; }

    /// <inheritdoc/>
    [Obsolete("Favor asynchronous version instead.")]
    public VerifyCheckResponse VerifyCheck(VerifyCheckRequest request, Credentials creds = null)
    {
        var response = ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObject<VerifyCheckResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/verify/check/json"),
                request
            );
        this.ValidateVerifyResponse(response);
        return response;
    }

    /// <inheritdoc/>
    public async Task<VerifyCheckResponse> VerifyCheckAsync(VerifyCheckRequest request, Credentials creds = null)
    {
        var response = await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObjectAsync<VerifyCheckResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/verify/check/json"),
                request
            );
        this.ValidateVerifyResponse(response);
        return response;
    }

    /// <inheritdoc/>
    [Obsolete("Favor asynchronous version instead.")]
    public VerifyControlResponse VerifyControl(VerifyControlRequest request, Credentials creds = null)
    {
        var response = ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObject<VerifyControlResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/verify/control/json"),
                request
            );
        this.ValidateVerifyResponse(response);
        return response;
    }

    /// <inheritdoc/>
    public async Task<VerifyControlResponse> VerifyControlAsync(VerifyControlRequest request, Credentials creds = null)
    {
        var response = await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObjectAsync<VerifyControlResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/verify/control/json"),
                request
            );
        this.ValidateVerifyResponse(response);
        return response;
    }

    /// <inheritdoc/>
    [Obsolete("Favor asynchronous version instead.")]
    public VerifyResponse VerifyRequest(VerifyRequest request, Credentials creds = null)
    {
        var response = ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObject<VerifyResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/verify/json"),
                request
            );
        this.ValidateVerifyResponse(response);
        return response;
    }

    /// <inheritdoc/>
    public async Task<VerifyResponse> VerifyRequestAsync(VerifyRequest request, Credentials creds = null)
    {
        var response = await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObjectAsync<VerifyResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/verify/json"),
                request
            );
        this.ValidateVerifyResponse(response);
        return response;
    }

    /// <inheritdoc/>
    [Obsolete("Favor asynchronous version instead.")]
    public VerifyResponse VerifyRequestWithPSD2(Psd2Request request, Credentials creds = null)
    {
        var response = ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObject<VerifyResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/verify/psd2/json"),
                request
            );
        this.ValidateVerifyResponse(response);
        return response;
    }

    /// <inheritdoc/>
    public async Task<VerifyResponse> VerifyRequestWithPSD2Async(Psd2Request request, Credentials creds = null)
    {
        var response = await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObjectAsync<VerifyResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/verify/psd2/json"),
                request
            );
        this.ValidateVerifyResponse(response);
        return response;
    }

    /// <inheritdoc/>
    [Obsolete("Favor asynchronous version instead.")]
    public VerifySearchResponse VerifySearch(VerifySearchRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParameters<VerifySearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/verify/search/json"),
                AuthType.Query,
                request
            );

    /// <inheritdoc/>
    public Task<VerifySearchResponse> VerifySearchAsync(VerifySearchRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<VerifySearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/verify/search/json"),
                AuthType.Query,
                request
            );

    public void ValidateVerifyResponse(VerifyResponseBase response)
    {
        if (response.Status != "0")
        {
            throw new VonageVerifyResponseException(
                    $"Verify Request Failed with status: {response.Status} and Error Text: {response.ErrorText}")
                {Response = response};
        }
    }

    private Credentials GetCredentials(Credentials overridenCredentials) => overridenCredentials ?? this.Credentials;
}