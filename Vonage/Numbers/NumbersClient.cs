using System;
using System.Threading.Tasks;
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
        
        public Task<NumbersSearchResponse> GetOwnedNumbersAsync(NumberSearchRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<NumbersSearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/account/numbers"), 
                ApiRequest.AuthType.Query, 
                request, 
                creds ?? Credentials
                );
        }

        public Task<NumbersSearchResponse> GetAvailableNumbersAsync(NumberSearchRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<NumbersSearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/number/search"), 
                ApiRequest.AuthType.Query, 
                request, 
                creds ?? Credentials
            );
        }

        public async Task<NumberTransactionResponse> BuyANumberAsync(NumberTransactionRequest request, Credentials creds = null)
        {
            var response = await ApiRequest.DoPostRequestUrlContentFromObjectAsync<NumberTransactionResponse>(                
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/number/buy"),
                request,                
                creds ?? Credentials
            );
            ValidateNumbersResponse(response);
            return response; 
        }

        public async Task<NumberTransactionResponse> CancelANumberAsync(NumberTransactionRequest request, Credentials creds = null)
        {
            var response = await ApiRequest.DoPostRequestUrlContentFromObjectAsync<NumberTransactionResponse>(                
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/number/cancel"),
                request,                
                creds ?? Credentials
            );
            ValidateNumbersResponse(response);
            return response;
        }

        public async Task<NumberTransactionResponse> UpdateANumberAsync(UpdateNumberRequest request, Credentials creds = null)
        {
            var response = await ApiRequest.DoPostRequestUrlContentFromObjectAsync<NumberTransactionResponse>(
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

        public NumbersSearchResponse GetOwnedNumbers(NumberSearchRequest request, Credentials creds = null)
        {
            return GetOwnedNumbersAsync(request, creds).GetAwaiter().GetResult();
        }

        public NumbersSearchResponse GetAvailableNumbers(NumberSearchRequest request, Credentials creds = null)
        {
            return GetAvailableNumbersAsync(request, creds).GetAwaiter().GetResult();
        }

        public NumberTransactionResponse BuyANumber(NumberTransactionRequest request, Credentials creds = null)
        {
            return BuyANumberAsync(request, creds).GetAwaiter().GetResult();
        }

        public NumberTransactionResponse CancelANumber(NumberTransactionRequest request, Credentials creds = null)
        {
            return CancelANumberAsync(request, creds).GetAwaiter().GetResult();
        }

        public NumberTransactionResponse UpdateANumber(UpdateNumberRequest request, Credentials creds = null)
        {
            return UpdateANumberAsync(request, creds).GetAwaiter().GetResult();
        }
    }
}