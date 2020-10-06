using System;
using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Accounts
{
    public class AccountClient : IAccountClient
    {
        public Credentials Credentials { get; set; }
        public AccountClient(Credentials creds = null)
        {
            Credentials = creds;
        }
        
        public Task<Balance> GetAccountBalanceAsync(Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<Balance>(
                ApiRequest.GetBaseUriFor(typeof(AccountClient),
                "/account/get-balance"), 
                ApiRequest.AuthType.Query, 
                credentials: creds ?? Credentials);
        }

        public Task<TopUpResult> TopUpAccountBalanceAsync(TopUpRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<TopUpResult>(
                ApiRequest.GetBaseUriFor(typeof(AccountClient), "/account/top-up"),
                ApiRequest.AuthType.Query,
                request,
                credentials:creds ?? Credentials
            );
        }

        public Task<AccountSettingsResult> ChangeAccountSettingsAsync(AccountSettingsRequest request, Credentials creds = null)
        {
            return ApiRequest.DoPostRequestUrlContentFromObjectAsync<AccountSettingsResult>
            (
                ApiRequest.GetBaseUriFor(typeof(AccountClient), "/account/settings"),
                request,
                creds ?? Credentials
            );
        }

        public Task<SecretsRequestResult> RetrieveApiSecretsAsync(string apiKey, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<SecretsRequestResult>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets"),
                ApiRequest.AuthType.Basic,
                credentials: creds ?? Credentials
            );
        }

        public Task<Secret> CreateApiSecretAsync(CreateSecretRequest request, string apiKey, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContentAsync<Secret>(
                "POST",
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets"),
                request,
                ApiRequest.AuthType.Basic,
                creds: creds ?? Credentials
            );
        }

        public Task<Secret> RetrieveApiSecretAsync(string secretId, string apiKey, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<Secret>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets/{secretId}"),
                ApiRequest.AuthType.Basic,
                credentials: creds ?? Credentials
            );
        }

        public async Task<bool> RevokeApiSecretAsync(string secretId, string apiKey, Credentials creds = null)
        {
            await ApiRequest.DoDeleteRequestWithUrlContentAsync(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets/{secretId}"),
                null,
                ApiRequest.AuthType.Basic,
                creds ?? Credentials
            );
            return true;
        }        
    }
}