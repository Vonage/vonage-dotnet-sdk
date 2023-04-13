using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.NumberInsights
{
    public class NumberInsightClient : INumberInsightClient
    {
        public Credentials Credentials { get; set; }

        public NumberInsightClient(Credentials creds = null)
        {
            Credentials = creds;
        }
        public async Task<BasicInsightResponse> GetNumberInsightBasicAsync(BasicNumberInsightRequest request, Credentials creds = null)
        {
            var response = await ApiRequest.DoGetRequestWithQueryParametersAsync<BasicInsightResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/basic/json"),
                AuthType.Query,
                request,
                creds ?? Credentials
            );
            ValidateNumberInsightResponse(response);
            return response;
        }

        public async Task<StandardInsightResponse> GetNumberInsightStandardAsync(StandardNumberInsightRequest request, Credentials creds = null)
        {
            var response = await ApiRequest.DoGetRequestWithQueryParametersAsync<StandardInsightResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/standard/json"),
                AuthType.Query,
                request,
                creds ?? Credentials
            );
            ValidateNumberInsightResponse(response);
            return response;
        }

        public async Task<AdvancedInsightsResponse> GetNumberInsightAdvancedAsync(AdvancedNumberInsightRequest request, Credentials creds = null)
        {
            var response = await ApiRequest.DoGetRequestWithQueryParametersAsync<AdvancedInsightsResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/advanced/json"),
                AuthType.Query,
                request,
                creds ?? Credentials
            );
            ValidateNumberInsightResponse(response);
            return response;
        }

        public async Task<AdvancedInsightsAsynchronousResponse> GetNumberInsightAsynchronousAsync(AdvancedNumberInsightAsynchronousRequest request, Credentials creds = null)
        {
            var response = await ApiRequest.DoGetRequestWithQueryParametersAsync<AdvancedInsightsAsynchronousResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/advanced/async/json"),
                AuthType.Query,
                request,
                creds ?? Credentials
            );
            ValidateNumberInsightResponse(response);
            return response;
        }

        public BasicInsightResponse GetNumberInsightBasic(BasicNumberInsightRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoGetRequestWithQueryParameters<BasicInsightResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/basic/json"),
                AuthType.Query,
                request,
                creds ?? Credentials
            );
            ValidateNumberInsightResponse(response);
            return response;
        }

        public StandardInsightResponse GetNumberInsightStandard(StandardNumberInsightRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoGetRequestWithQueryParameters<StandardInsightResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/standard/json"),
                AuthType.Query,
                request,
                creds ?? Credentials
            );
            ValidateNumberInsightResponse(response);
            return response;
        }

        public AdvancedInsightsResponse GetNumberInsightAdvanced(AdvancedNumberInsightRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoGetRequestWithQueryParameters<AdvancedInsightsResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/advanced/json"),
                AuthType.Query,
                request,
                creds ?? Credentials
            );
            ValidateNumberInsightResponse(response);
            return response;
        }

        public AdvancedInsightsAsynchronousResponse GetNumberInsightAsynchronous(AdvancedNumberInsightAsynchronousRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoGetRequestWithQueryParameters<AdvancedInsightsAsynchronousResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/advanced/async/json"),
                AuthType.Query,
                request,
                creds ?? Credentials
            );
            ValidateNumberInsightResponse(response);
            return response;
        }

        public void ValidateNumberInsightResponse(NumberInsightResponseBase response)
        {
            if (response.Status != 0)
            {
                switch (response)
                {
                    case AdvancedInsightsAsynchronousResponse asyncResponse:
                        throw new VonageNumberInsightResponseException($"Advanced Insights Async response failed with status: {asyncResponse.Status}") { Response = response};
                    case BasicInsightResponse basicInsightResponse:
                        throw new VonageNumberInsightResponseException($"Number insight request failed with status: {basicInsightResponse.Status} and error message: {basicInsightResponse.StatusMessage}") { Response=response};
                }
            }
        }
    }
}