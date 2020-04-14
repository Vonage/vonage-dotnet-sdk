using System;
using Nexmo.Api.Request;

namespace Nexmo.Api.Accounts
{
    public class AccountClient : IAccountClient
    {
        public Credentials Credentials { get; set; }
        public AccountClient(Credentials creds)
        {
            Credentials = creds;
        }
        
        public Balance GetAccountBalance(Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithUrlContent<Balance>(
                ApiRequest.GetBaseUriFor(typeof(AccountClient),
                "/account/get-balance"), 
                ApiRequest.AuthType.Query, 
                credentials: creds ?? Credentials);
        }

        public TopUpResult TopUpAccountBalance(TopUpRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithUrlContent<TopUpResult>(
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

        public SecretsRequestResult RetrieveApiSecrets(string apiKey = null, Credentials creds = null)
        {
            apiKey = apiKey ?? (creds?.ApiKey ?? Credentials.ApiKey);
            apiKey = apiKey ?? Configuration.Instance.Settings["appSettings:Nexmo.api_key"];
            return ApiRequest.DoGetRequestWithUrlContent<SecretsRequestResult>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets"),
                ApiRequest.AuthType.Basic,
                credentials: creds ?? Credentials
            );
        }

        public Secret CreateApiSecret(CreateSecretRequest request, string apiKey = null, Credentials creds = null)
        {
            apiKey = apiKey ?? (creds?.ApiKey ?? Credentials.ApiKey);
            apiKey = apiKey ?? Configuration.Instance.Settings["appSettings:Nexmo.api_key"];
            return ApiRequest.DoRequestWithJsonContent<Secret>(
                "POST",
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets"),
                request,
                ApiRequest.AuthType.Basic,
                creds: creds ?? Credentials
            );
        }

        public Secret RetrieveApiSecret(string secretId, string apiKey = null, Credentials creds = null)
        {
            apiKey = apiKey ?? (creds?.ApiKey ?? Credentials.ApiKey);
            apiKey = apiKey ?? Configuration.Instance.Settings["appSettings:Nexmo.api_key"];
            return ApiRequest.DoGetRequestWithUrlContent<Secret>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets/{secretId}"),
                ApiRequest.AuthType.Basic,
                credentials: creds ?? Credentials
            );
        }

        public bool RevokeApiSecret(string secretId, string apiKey = null, Credentials creds = null)
        {
            apiKey = apiKey ?? (creds?.ApiKey ?? Credentials.ApiKey);
            apiKey = apiKey ?? Configuration.Instance.Settings["appSettings:Nexmo.api_key"];
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