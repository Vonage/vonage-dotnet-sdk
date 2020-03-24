using Nexmo.Api.Request;

namespace Nexmo.Api.NumberInsights
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
            return ApiRequest.DoGetRequestWithUrlContent<BasicInsightResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/basic/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials
            );
        }

        public StandardInsightResponse GetNumberInsightStandard(StandardNumberInsightRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithUrlContent<StandardInsightResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/standard/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials
            );
        }

        public AdvancedInsightsResponse GetNumberInsightAdvanced(AdvancedNumberInsightRequest request,
            Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithUrlContent<AdvancedInsightsResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/advanced/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials
            );
        }

        public AdvancedInsightsAsyncResponse GetNumberInsightAsync(AdvancedNumberInsightAsynchronousRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithUrlContent<AdvancedInsightsAsyncResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/ni/advanced/async/json"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials
            );
        }
    }
}