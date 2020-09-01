using System;
using Nexmo.Api.Request;

namespace Nexmo.Api.Accounts
{
    [Obsolete("The Nexmo.Api.Accounts.AccountClient class is obsolete. " +
        "References to it should be switched to the new Vonage.Accounts.AccountClient class.")]
    public class AccountClient : IAccountClient
    {
        public Credentials Credentials { get; set; }
        public AccountClient(Credentials creds)
        {
            Credentials = creds;
        }
        
        public Balance GetAccountBalance(Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<Balance>(
                ApiRequest.GetBaseUriFor(typeof(AccountClient),
                "/account/get-balance"), 
                ApiRequest.AuthType.Query, 
                credentials: creds ?? Credentials);
        }

        public TopUpResult TopUpAccountBalance(TopUpRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<TopUpResult>(
                ApiRequest.GetBaseUriFor(typeof(AccountClient), "/account/top-up"),
                ApiRequest.AuthType.Query,
                request,
                credentials:creds ?? Credentials
            );
        }

        public AccountSettingsResult ChangeAccountSettings(AccountSettingsRequest request, Credentials creds = null)
        {
            return ApiRequest.DoPostRequestUrlContentFromObject<AccountSettingsResult>
            (
                ApiRequest.GetBaseUriFor(typeof(AccountClient), "/account/settings"),
                request,
                creds ?? Credentials
            );
        }

        public SecretsRequestResult RetrieveApiSecrets(string apiKey, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<SecretsRequestResult>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets"),
                ApiRequest.AuthType.Basic,
                credentials: creds ?? Credentials
            );
        }

        public Secret CreateApiSecret(CreateSecretRequest request, string apiKey, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContent<Secret>(
                "POST",
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets"),
                request,
                ApiRequest.AuthType.Basic,
                creds: creds ?? Credentials
            );
        }

        public Secret RetrieveApiSecret(string secretId, string apiKey, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<Secret>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets/{secretId}"),
                ApiRequest.AuthType.Basic,
                credentials: creds ?? Credentials
            );
        }

        public bool RevokeApiSecret(string secretId, string apiKey, Credentials creds = null)
        {
            ApiRequest.DoDeleteRequestWithUrlContent(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets/{secretId}"),
                null,
                ApiRequest.AuthType.Basic,
                creds ?? Credentials
            );
            return true;
        }        
    }
}