#region
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Request;
#endregion

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
    public async Task<VerifyCheckResponse> VerifyCheckAsync(VerifyCheckRequest request, Credentials creds = null)
    {
        var response = await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObjectAsync<VerifyCheckResponse>(
                this.configuration.GetBaseUri(ApiRequest.UriType.Api, "/verify/check/json"),
                request
            ).ConfigureAwait(false);
        this.ValidateVerifyResponse(response);
        return response;
    }

    /// <inheritdoc/>
    public async Task<VerifyControlResponse> VerifyControlAsync(VerifyControlRequest request, Credentials creds = null)
    {
        var response = await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObjectAsync<VerifyControlResponse>(
                this.configuration.GetBaseUri(ApiRequest.UriType.Api, "/verify/control/json"),
                request
            ).ConfigureAwait(false);
        this.ValidateVerifyResponse(response);
        return response;
    }

    /// <inheritdoc/>
    public async Task<VerifyResponse> VerifyRequestAsync(VerifyRequest request, Credentials creds = null)
    {
        var response = await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObjectAsync<VerifyResponse>(
                this.configuration.GetBaseUri(ApiRequest.UriType.Api, "/verify/json"),
                request
            ).ConfigureAwait(false);
        this.ValidateVerifyResponse(response);
        return response;
    }

    /// <inheritdoc/>
    public async Task<VerifyResponse> VerifyRequestWithPSD2Async(Psd2Request request, Credentials creds = null)
    {
        var response = await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObjectAsync<VerifyResponse>(
                this.configuration.GetBaseUri(ApiRequest.UriType.Api, "/verify/psd2/json"),
                request
            ).ConfigureAwait(false);
        this.ValidateVerifyResponse(response);
        return response;
    }

    /// <inheritdoc/>
    public Task<VerifySearchResponse> VerifySearchAsync(VerifySearchRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<VerifySearchResponse>(
                this.configuration.GetBaseUri(ApiRequest.UriType.Api, "/verify/search/json"),
                AuthType.Basic,
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