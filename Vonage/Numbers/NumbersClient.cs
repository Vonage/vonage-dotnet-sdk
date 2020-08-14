using System;
using Vonage.Request;

namespace Vonage.Numbers
{
    public class NumbersClient : INumbersClient
    {
        public NumbersClient(Credentials creds = null)
        {
            Credentials = creds;
        }
        
        public Credentials Credentials { get; set; }
        
        public NumbersSearchResponse GetOwnedNumbers(NumberSearchRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<NumbersSearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/account/numbers"), 
                ApiRequest.AuthType.Query, 
                request, 
                creds ?? Credentials
                );
        }

        public NumbersSearchResponse GetAvailableNumbers(NumberSearchRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<NumbersSearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/number/search"), 
                ApiRequest.AuthType.Query, 
                request, 
                creds ?? Credentials
            );
        }

        public NumberTransactionResponse BuyANumber(NumberTransactionRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequestUrlContentFromObject<NumberTransactionResponse>(                
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/number/buy"),
                request,                
                creds ?? Credentials
            );
            ValidateNumbersResponse(response);
            return response; 
        }

        public NumberTransactionResponse CancelANumber(NumberTransactionRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequestUrlContentFromObject<NumberTransactionResponse>(                
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/number/cancel"),
                request,                
                creds ?? Credentials
            );
            ValidateNumbersResponse(response);
            return response;
        }

        public NumberTransactionResponse UpdateANumber(UpdateNumberRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequestUrlContentFromObject<NumberTransactionResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/number/update"),
                request,
                creds ?? Credentials
            );
            ValidateNumbersResponse(response);
            return response;
        }

        public static void ValidateNumbersResponse(NumberTransactionResponse response)
        {
            const string SUCCESS = "200";
            if (response.ErrorCode != SUCCESS)
            {
                throw new VonageNumberResponseException($"Number Transaction failed with error code:{response.ErrorCode} and label {response.ErrorCodeLabel}"){ Response = response};
            }
        }
    }
}