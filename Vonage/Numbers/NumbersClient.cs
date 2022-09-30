using System;
using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Numbers
{
    public class NumbersClient : INumbersClient
    {
        public NumbersClient(Credentials creds = null, int? timeout = null)
        {
            Credentials = creds;
            Timeout = timeout;
        }
        
        public Credentials Credentials { get; set; }
        public int? Timeout { get; private set; }

        public Task<NumbersSearchResponse> GetOwnedNumbersAsync(NumberSearchRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<NumbersSearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/account/numbers"), 
                ApiRequest.AuthType.Query, 
                request, 
                creds ?? Credentials,
                timeout: Timeout
                );
        }

        public Task<NumbersSearchResponse> GetAvailableNumbersAsync(NumberSearchRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<NumbersSearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/number/search"), 
                ApiRequest.AuthType.Query, 
                request, 
                creds ?? Credentials,
                timeout: Timeout
            );
        }

        public async Task<NumberTransactionResponse> BuyANumberAsync(NumberTransactionRequest request, Credentials creds = null)
        {
            var response = await ApiRequest.DoPostRequestUrlContentFromObjectAsync<NumberTransactionResponse>(                
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/number/buy"),
                request,                
                creds ?? Credentials,
                timeout: Timeout
            );
            ValidateNumbersResponse(response);
            return response; 
        }

        public async Task<NumberTransactionResponse> CancelANumberAsync(NumberTransactionRequest request, Credentials creds = null)
        {
            var response = await ApiRequest.DoPostRequestUrlContentFromObjectAsync<NumberTransactionResponse>(                
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/number/cancel"),
                request,                
                creds ?? Credentials,
                timeout: Timeout
            );
            ValidateNumbersResponse(response);
            return response;
        }

        public async Task<NumberTransactionResponse> UpdateANumberAsync(UpdateNumberRequest request, Credentials creds = null)
        {
            var response = await ApiRequest.DoPostRequestUrlContentFromObjectAsync<NumberTransactionResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/number/update"),
                request,
                creds ?? Credentials,
                timeout: Timeout
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

        public NumbersSearchResponse GetOwnedNumbers(NumberSearchRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<NumbersSearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/account/numbers"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials,
                timeout: Timeout
                );
        }

        public NumbersSearchResponse GetAvailableNumbers(NumberSearchRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<NumbersSearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/number/search"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials,
                timeout: Timeout
            );
        }

        public NumberTransactionResponse BuyANumber(NumberTransactionRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequestUrlContentFromObject<NumberTransactionResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/number/buy"),
                request,
                creds ?? Credentials,
                timeout: Timeout
            );
            ValidateNumbersResponse(response);
            return response;
        }

        public NumberTransactionResponse CancelANumber(NumberTransactionRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequestUrlContentFromObject<NumberTransactionResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/number/cancel"),
                request,
                creds ?? Credentials,
                timeout: Timeout
            );
            ValidateNumbersResponse(response);
            return response;
        }

        public NumberTransactionResponse UpdateANumber(UpdateNumberRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequestUrlContentFromObject<NumberTransactionResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/number/update"),
                request,
                creds ?? Credentials,
                timeout: Timeout
            );
            ValidateNumbersResponse(response);
            return response;
        }
    }
}