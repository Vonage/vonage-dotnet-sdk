using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.NumberInsights;

public class NumberInsightClient : INumberInsightClient
{
    public Credentials Credentials { get; set; }

    public NumberInsightClient(Credentials creds = null)
    {
        this.Credentials = creds;
    }

    public AdvancedInsightsResponse GetNumberInsightAdvanced(AdvancedNumberInsightRequest request,
        Credentials creds = null)
    {
        var response = new ApiRequest(creds ?? this.Credentials)
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
        var response = await new ApiRequest(creds ?? this.Credentials)
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
        var response = new ApiRequest(creds ?? this.Credentials)
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
        var response = await new ApiRequest(creds ?? this.Credentials)
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
        var response = new ApiRequest(creds ?? this.Credentials).DoGetRequestWithQueryParameters<BasicInsightResponse>(
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
        var response = await new ApiRequest(creds ?? this.Credentials)
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
        var response = new ApiRequest(creds ?? this.Credentials)
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
        var response = await new ApiRequest(creds ?? this.Credentials)
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
}