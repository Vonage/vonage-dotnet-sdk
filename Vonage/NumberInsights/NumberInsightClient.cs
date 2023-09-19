using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.NumberInsights;

public class NumberInsightClient : INumberInsightClient
{
    private readonly Configuration configuration;
    public Credentials Credentials { get; set; }

    public NumberInsightClient(Credentials creds = null)
    {
        this.Credentials = creds;
        this.configuration = Configuration.Instance;
    }

    internal NumberInsightClient(Credentials credentials, Configuration configuration)
    {
        this.Credentials = credentials;
        this.configuration = configuration;
    }

    public AdvancedInsightsResponse GetNumberInsightAdvanced(AdvancedNumberInsightRequest request,
        Credentials creds = null)
    {
        var response = ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoGetRequestWithQueryParameters<AdvancedInsightsResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/advanced/json"),
                AuthType.Query,
                request
            );
        this.ValidateNumberInsightResponse(response);
        return response;
    }

    public async Task<AdvancedInsightsResponse> GetNumberInsightAdvancedAsync(AdvancedNumberInsightRequest request,
        Credentials creds = null)
    {
        var response = await ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoGetRequestWithQueryParametersAsync<AdvancedInsightsResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/advanced/json"),
                AuthType.Query,
                request
            );
        this.ValidateNumberInsightResponse(response);
        return response;
    }

    public AdvancedInsightsAsynchronousResponse GetNumberInsightAsynchronous(
        AdvancedNumberInsightAsynchronousRequest request, Credentials creds = null)
    {
        var response = ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoGetRequestWithQueryParameters<AdvancedInsightsAsynchronousResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/advanced/async/json"),
                AuthType.Query,
                request
            );
        this.ValidateNumberInsightResponse(response);
        return response;
    }

    public async Task<AdvancedInsightsAsynchronousResponse> GetNumberInsightAsynchronousAsync(
        AdvancedNumberInsightAsynchronousRequest request, Credentials creds = null)
    {
        var response = await ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoGetRequestWithQueryParametersAsync<AdvancedInsightsAsynchronousResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/advanced/async/json"),
                AuthType.Query,
                request
            );
        this.ValidateNumberInsightResponse(response);
        return response;
    }

    public BasicInsightResponse GetNumberInsightBasic(BasicNumberInsightRequest request, Credentials creds = null)
    {
        var response = ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoGetRequestWithQueryParameters<BasicInsightResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/basic/json"),
                AuthType.Query,
                request
            );
        this.ValidateNumberInsightResponse(response);
        return response;
    }

    public async Task<BasicInsightResponse> GetNumberInsightBasicAsync(BasicNumberInsightRequest request,
        Credentials creds = null)
    {
        var response = await ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoGetRequestWithQueryParametersAsync<BasicInsightResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/basic/json"),
                AuthType.Query,
                request
            );
        this.ValidateNumberInsightResponse(response);
        return response;
    }

    public StandardInsightResponse GetNumberInsightStandard(StandardNumberInsightRequest request,
        Credentials creds = null)
    {
        var response = ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoGetRequestWithQueryParameters<StandardInsightResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/standard/json"),
                AuthType.Query,
                request
            );
        this.ValidateNumberInsightResponse(response);
        return response;
    }

    public async Task<StandardInsightResponse> GetNumberInsightStandardAsync(StandardNumberInsightRequest request,
        Credentials creds = null)
    {
        var response = await ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoGetRequestWithQueryParametersAsync<StandardInsightResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/standard/json"),
                AuthType.Query,
                request
            );
        this.ValidateNumberInsightResponse(response);
        return response;
    }

    public void ValidateNumberInsightResponse(NumberInsightResponseBase response)
    {
        if (response.Status != 0)
        {
            switch (response)
            {
                case AdvancedInsightsAsynchronousResponse asyncResponse:
                    throw new VonageNumberInsightResponseException(
                            $"Advanced Insights Async response failed with status: {asyncResponse.Status}")
                        {Response = response};
                case BasicInsightResponse basicInsightResponse:
                    throw new VonageNumberInsightResponseException(
                            $"Number insight request failed with status: {basicInsightResponse.Status} and error message: {basicInsightResponse.StatusMessage}")
                        {Response = response};
            }
        }
    }

    private Credentials GetCredentials(Credentials overridenCredentials) => overridenCredentials ?? this.Credentials;
}