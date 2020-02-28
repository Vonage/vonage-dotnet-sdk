using System;
using Nexmo.Api.Request;

namespace Nexmo.Api.Numbers
{
    public class NumbersClient : INumbersClient
    {
        public NumbersClient(Credentials creds)
        {
            Credentials = creds;
        }
        
        public Credentials Credentials { get; set; }
        
        public NumbersSearchResponse GetOwnedNumbers(NumberSearchRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithUrlContent<NumbersSearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/account/numbers"), 
                ApiRequest.AuthType.Query, 
                request, 
                creds ?? Credentials
                );
        }

        public NumbersSearchResponse GetAvailableNumbers(NumberSearchRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithUrlContent<NumbersSearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/number/search"), 
                ApiRequest.AuthType.Query, 
                request, 
                creds ?? Credentials
            );
        }

        public NumberTransactionResponse BuyANumber(NumberTransactionRequest request, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContent<NumberTransactionResponse>(
                "POST",
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/number/buy"),
                request,
                ApiRequest.AuthType.Query,
                creds ?? Credentials
            );
        }

        public NumberTransactionResponse CancelANumber(NumberTransactionRequest request, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContent<NumberTransactionResponse>(
                "POST",
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/number/cancel"),
                request,
                ApiRequest.AuthType.Query,
                creds ?? Credentials
            );
        }

        public NumberTransactionResponse UpdateANumber(UpdateNumberRequest request, Credentials creds = null)
        {
            return ApiRequest.DoPostRequestUrlContentFromObject<NumberTransactionResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/number/update"),
                request,
                creds ?? Credentials
            );
        }
    }
}