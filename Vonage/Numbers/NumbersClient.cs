using System;
using System.Collections.Generic;
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
            var apiKey = (creds ?? Credentials).ApiKey;
            var apiSecret = (creds ?? Credentials).ApiSecret;
            var response = await ApiRequest.DoPostRequestUrlContentFromObjectAsync<NumberTransactionResponse>(                
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/number/buy?api_key={apiKey}&api_secret={apiSecret}"),
                request,                
                creds ?? Credentials,
                false
            );
            ValidateNumbersResponse(response);
            return response; 
        }

        public async Task<NumberTransactionResponse> CancelANumberAsync(NumberTransactionRequest request, Credentials creds = null)
        {
            var apiKey = (creds ?? Credentials).ApiKey;
            var apiSecret = (creds ?? Credentials).ApiSecret;
            var response = await ApiRequest.DoPostRequestUrlContentFromObjectAsync<NumberTransactionResponse>(                
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/number/cancel?api_key={apiKey}&api_secret={apiSecret}"),
                request,                
                creds ?? Credentials,
                false
            );
            ValidateNumbersResponse(response);
            return response;
        }

        public async Task<NumberTransactionResponse> UpdateANumberAsync(UpdateNumberRequest request, Credentials creds = null)
        {
            var apiKey = (creds ?? Credentials).ApiKey;
            var apiSecret = (creds ?? Credentials).ApiSecret;
            var response = await ApiRequest.DoPostRequestUrlContentFromObjectAsync<NumberTransactionResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/number/update?api_key={apiKey}&api_secret={apiSecret}"),
                request,
                creds ?? Credentials,
                false
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
            var apiKey = (creds ?? Credentials).ApiKey;
            var apiSecret = (creds ?? Credentials).ApiSecret;
            var response = ApiRequest.DoPostRequestUrlContentFromObject<NumberTransactionResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/number/buy?api_key={apiKey}&api_secret={apiSecret}"),
                request,
                creds ?? Credentials,
                false
            );
            ValidateNumbersResponse(response);
            return response;
        }

        public NumberTransactionResponse CancelANumber(NumberTransactionRequest request, Credentials creds = null)
        {
            var apiKey = (creds ?? Credentials).ApiKey;
            var apiSecret = (creds ?? Credentials).ApiSecret;
            var response = ApiRequest.DoPostRequestUrlContentFromObject<NumberTransactionResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/number/cancel?api_key={apiKey}&api_secret={apiSecret}"),
                request,
                creds ?? Credentials,
                false
            );
            ValidateNumbersResponse(response);
            return response;
        }

        public NumberTransactionResponse UpdateANumber(UpdateNumberRequest request, Credentials creds = null)
        {
            var apiKey = (creds ?? Credentials).ApiKey;
            var apiSecret = (creds ?? Credentials).ApiSecret;
            var response = ApiRequest.DoPostRequestUrlContentFromObject<NumberTransactionResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, $"/number/update?api_key={apiKey}&api_secret={apiSecret}"),
                request,
                creds ?? Credentials,
                false
            );
            ValidateNumbersResponse(response);
            return response;
        }
        
        public NumberTransferResponse TransferANumber(NumberTransferRequest request, string apiKey, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContent<NumberTransferResponse>(
                "POST",
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/transfer-number"),
                request,
                ApiRequest.AuthType.Basic,
                creds: creds ?? Credentials
            );
        }
        
        public Task<NumberTransferResponse> TransferANumberAsync(NumberTransferRequest request, string apiKey, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContentAsync<NumberTransferResponse>(
                "POST",
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/transfer-number"),
                request,
                ApiRequest.AuthType.Basic,
                creds: creds ?? Credentials
            );
        }
    }
}