using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Accounts;

public class AccountClient : IAccountClient
{
    public Credentials Credentials { get; set; }
    public AccountClient(Credentials creds = null)
    {
        this.Credentials = creds;
    }
        
    public Task<Balance> GetAccountBalanceAsync(Credentials creds = null)
    {
        return ApiRequest.DoGetRequestWithQueryParametersAsync<Balance>(
            ApiRequest.GetBaseUriFor("/account/get-balance"), 
            ApiRequest.AuthType.Query, 
            credentials: creds ?? this.Credentials);
    }

    public Task<TopUpResult> TopUpAccountBalanceAsync(TopUpRequest request, Credentials creds = null)
    {
        return ApiRequest.DoGetRequestWithQueryParametersAsync<TopUpResult>(
            ApiRequest.GetBaseUriFor("/account/top-up"),
            ApiRequest.AuthType.Query,
            request,
            credentials:creds ?? this.Credentials
        );
    }

    public Task<AccountSettingsResult> ChangeAccountSettingsAsync(AccountSettingsRequest request, Credentials creds = null)
    {
        return ApiRequest.DoPostRequestUrlContentFromObjectAsync<AccountSettingsResult>
        (
            ApiRequest.GetBaseUriFor("/account/settings"),
            request,
            creds ?? this.Credentials
        );
    }

    public Task<SecretsRequestResult> RetrieveApiSecretsAsync(string apiKey, Credentials creds = null)
    {
        return ApiRequest.DoGetRequestWithQueryParametersAsync<SecretsRequestResult>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets"),
            ApiRequest.AuthType.Basic,
            credentials: creds ?? this.Credentials
        );
    }

    public Task<Secret> CreateApiSecretAsync(CreateSecretRequest request, string apiKey, Credentials creds = null)
    {
        return ApiRequest.DoRequestWithJsonContentAsync<Secret>(
            "POST",
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets"),
            request,
            ApiRequest.AuthType.Basic,
            creds: creds ?? this.Credentials
        );
    }

    public Task<Secret> RetrieveApiSecretAsync(string secretId, string apiKey, Credentials creds = null)
    {
        return ApiRequest.DoGetRequestWithQueryParametersAsync<Secret>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets/{secretId}"),
            ApiRequest.AuthType.Basic,
            credentials: creds ?? this.Credentials
        );
    }

    public async Task<bool> RevokeApiSecretAsync(string secretId, string apiKey, Credentials creds = null)
    {
        await ApiRequest.DoDeleteRequestWithUrlContentAsync(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets/{secretId}"),
            null,
            ApiRequest.AuthType.Basic,
            creds ?? this.Credentials
        );
        return true;
    }

    public Balance GetAccountBalance(Credentials creds = null)
    {
        return ApiRequest.DoGetRequestWithQueryParameters<Balance>(
            ApiRequest.GetBaseUriFor("/account/get-balance"),
            ApiRequest.AuthType.Query,
            credentials: creds ?? this.Credentials);
    }

    public TopUpResult TopUpAccountBalance(TopUpRequest request, Credentials creds = null)
    {
        return ApiRequest.DoGetRequestWithQueryParameters<TopUpResult>(
            ApiRequest.GetBaseUriFor("/account/top-up"),
            ApiRequest.AuthType.Query,
            request,
            credentials: creds ?? this.Credentials
        );
    }

    public AccountSettingsResult ChangeAccountSettings(AccountSettingsRequest request, Credentials creds = null)
    {
        return ApiRequest.DoPostRequestUrlContentFromObject<AccountSettingsResult>
        (
            ApiRequest.GetBaseUriFor("/account/settings"),
            request,
            creds ?? this.Credentials
        );
    }

    public SecretsRequestResult RetrieveApiSecrets(string apiKey = null, Credentials creds = null)
    {
        return ApiRequest.DoGetRequestWithQueryParameters<SecretsRequestResult>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets"),
            ApiRequest.AuthType.Basic,
            credentials: creds ?? this.Credentials
        );
    }

    public Secret CreateApiSecret(CreateSecretRequest request, string apiKey = null, Credentials creds = null)
    {
        return ApiRequest.DoRequestWithJsonContent<Secret>(
            "POST",
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets"),
            request,
            ApiRequest.AuthType.Basic,
            creds: creds ?? this.Credentials
        );
    }

    public Secret RetrieveApiSecret(string secretId, string apiKey = null, Credentials creds = null)
    {
        return ApiRequest.DoGetRequestWithQueryParameters<Secret>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets/{secretId}"),
            ApiRequest.AuthType.Basic,
            credentials: creds ?? this.Credentials
        );
    }

    public bool RevokeApiSecret(string secretId, string apiKey = null, Credentials creds = null)
    {
        ApiRequest.DoDeleteRequestWithUrlContent(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets/{secretId}"),
            null,
            ApiRequest.AuthType.Basic,
            creds ?? this.Credentials
        );
        return true;
    }
        
    public SubAccount CreateSubAccount(CreateSubAccountRequest request, string apiKey = null, Credentials creds = null)
    {
        return ApiRequest.DoRequestWithJsonContent<SubAccount>(
            "POST",
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/subaccounts"),
            request,
            ApiRequest.AuthType.Basic,
            creds: creds ?? this.Credentials
        );
    }
        
    public Task<SubAccount> CreateSubAccountAsync(CreateSubAccountRequest request, string apiKey = null, Credentials creds = null)
    {
        return ApiRequest.DoRequestWithJsonContentAsync<SubAccount>(
            "POST",
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/subaccounts"),
            request,
            ApiRequest.AuthType.Basic,
            creds: creds ?? this.Credentials
        );
    }
        
    public SubAccount RetrieveSubAccount(string subAccountKey, string apiKey = null, Credentials creds = null)
    {
        return ApiRequest.DoGetRequestWithQueryParameters<SubAccount>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/subaccounts/{subAccountKey}"),
            ApiRequest.AuthType.Basic,
            credentials: creds ?? this.Credentials
        );
    }
        
    public Task<SubAccount> RetrieveSubAccountAsync(string subAccountKey, string apiKey = null, Credentials creds = null)
    {
        return ApiRequest.DoGetRequestWithQueryParametersAsync<SubAccount>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/subaccounts/{subAccountKey}"),
            ApiRequest.AuthType.Basic,
            credentials: creds ?? this.Credentials
        );
    }
}