using Nexmo.Api.Request;

namespace Nexmo.Api.NumberInsights
{
    [System.Obsolete("The Nexmo.Api.NumberInsights.NumberInsightClient class is obsolete. " +
        "References to it should be switched to the new Vonage.NumberInsights.NumberInsightClient class.")]
    public class NumberInsightClient : INumberInsightClient
    {
        public Credentials Credentials { get; set; }

        public NumberInsightClient(Credentials creds = null)
        {
            Credentials = creds;
        }
        public BasicInsightResponse GetNumberInsightBasic(BasicNumberInsightRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoGetRequestWithQueryParameters<BasicInsightResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/basic/json"),
                ApiRequest.AuthType.Query,
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
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials
            );
            ValidateNumberInsightResponse(response);
            return response;
        }

        public AdvancedInsightsResponse GetNumberInsightAdvanced(AdvancedNumberInsightRequest request,
            Credentials creds = null)
        {
            var response = ApiRequest.DoGetRequestWithQueryParameters<AdvancedInsightsResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/advanced/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials
            );
            ValidateNumberInsightResponse(response);
            return response;
        }

        public AdvancedInsightsAsyncResponse GetNumberInsightAsync(AdvancedNumberInsightAsynchronousRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoGetRequestWithQueryParameters<AdvancedInsightsAsyncResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/advanced/async/json"),
                ApiRequest.AuthType.Query,
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
                if(response is AdvancedInsightsAsyncResponse asyncResponse)
                {
                    throw new NexmoNumberInsightResponseException($"Advanced Insights Async response failed with status: {asyncResponse.Status}") { Response = response};
                }
                else if(response is BasicInsightResponse basicInsightResponse)
                {                    
                    throw new NexmoNumberInsightResponseException($"Number insight request failed with status: {basicInsightResponse.Status} and error message: {basicInsightResponse.StatusMessage}") { Response=response};
                }
            }
        }
    }
}