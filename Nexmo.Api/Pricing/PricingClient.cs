using Nexmo.Api.Request;

namespace Nexmo.Api.Pricing
{
    public class PricingClient : IPricingClient
    {
        public PricingClient(Credentials creds)
        {
            Credentials = creds;
        }
        
        public Credentials Credentials { get; set; }
        
        public Country RetrievePricingCountry(string type, PricingCountryRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<Country>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials
            );
        }

        public PricingResult RetrievePricingAllCountries(string type, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<PricingResult>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-pricing/outbound/{type}"),
                ApiRequest.AuthType.Query,
                credentials: creds ?? Credentials
            );
        }

        public PricingResult RetrievePrefixPricing(string type, PricingPrefixRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<PricingResult>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/account/get-prefix-pricing/outbound/{type}"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials
            );
        }
    }
}