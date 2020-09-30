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
        public BasicInsightResponse GetNumberInsightBasic(BasicNumberInsightRequest request, Credentials creds = null)
        {
            return GetNumberInsightBasicAsync(request, creds).GetAwaiter().GetResult();
        }

        public StandardInsightResponse GetNumberInsightStandard(StandardNumberInsightRequest request, Credentials creds = null)
        {
            return GetNumberInsightStandardAsync(request, creds).GetAwaiter().GetResult();
        }

        public AdvancedInsightsResponse GetNumberInsightAdvanced(AdvancedNumberInsightRequest request,
            Credentials creds = null)
        {
            return AsyncGetNumberInsightAdvanced(request, creds).GetAwaiter().GetResult();
        }

        public AdvancedInsightsAsyncResponse GetNumberInsightAsync(AdvancedNumberInsightAsynchronousRequest request, Credentials creds = null)
        {
            return AsyncGetNumberInsightAsync(request, creds).GetAwaiter().GetResult();
        }

        public void ValidateNumberInsightResponse(NumberInsightResponseBase response)
        {
            if (response.Status != 0)
            {
                if(response is AdvancedInsightsAsyncResponse asyncResponse)
                {
                    throw new VonageNumberInsightResponseException($"Advanced Insights Async response failed with status: {asyncResponse.Status}") { Response = response};
                }
                else if(response is BasicInsightResponse basicInsightResponse)
                {                    
                    throw new VonageNumberInsightResponseException($"Number insight request failed with status: {basicInsightResponse.Status} and error message: {basicInsightResponse.StatusMessage}") { Response=response};
                }
            }
        }

        public async Task<BasicInsightResponse> GetNumberInsightBasicAsync(BasicNumberInsightRequest request, Credentials creds = null)
        {
            var response = await ApiRequest.DoGetRequestWithQueryParametersAsync<BasicInsightResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/basic/json"),
                ApiRequest.AuthType.Query,
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
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials
            );
            ValidateNumberInsightResponse(response);
            return response;
        }

        public async Task<AdvancedInsightsResponse> AsyncGetNumberInsightAdvanced(AdvancedNumberInsightRequest request, Credentials creds = null)
        {
            var response = await ApiRequest.DoGetRequestWithQueryParametersAsync<AdvancedInsightsResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/advanced/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials
            );
            ValidateNumberInsightResponse(response);
            return response;
        }

        public async Task<AdvancedInsightsAsyncResponse> AsyncGetNumberInsightAsync(AdvancedNumberInsightAsynchronousRequest request, Credentials creds = null)
        {
            var response = await ApiRequest.DoGetRequestWithQueryParametersAsync<AdvancedInsightsAsyncResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/advanced/async/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials
            );
            ValidateNumberInsightResponse(response);
            return response;
        }
    }
}