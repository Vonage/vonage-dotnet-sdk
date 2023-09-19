using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Request;

namespace Vonage.Accounts;

public class AccountClient : IAccountClient
{
    private readonly Configuration configuration;
    public Credentials Credentials { get; set; }

    public AccountClient(Credentials creds = null)
    {
        this.Credentials = creds;
        this.configuration = Configuration.Instance;
    }

    internal AccountClient(Credentials credentials, Configuration configuration)
    {
        this.Credentials = credentials;
        this.configuration = configuration;
    }

    public AccountSettingsResult ChangeAccountSettings(AccountSettingsRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoPostRequestUrlContentFromObject<AccountSettingsResult>
        (
            ApiRequest.GetBaseUriFor("/account/settings"),
            request
        );

    public Task<AccountSettingsResult> ChangeAccountSettingsAsync(AccountSettingsRequest request,
        Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoPostRequestUrlContentFromObjectAsync<AccountSettingsResult>
        (
            ApiRequest.GetBaseUriFor("/account/settings"),
            request
        );

    public Secret CreateApiSecret(CreateSecretRequest request, string apiKey = null, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoRequestWithJsonContent<Secret>(
            HttpMethod.Post,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets"),
            request,
            AuthType.Basic
        );

    public Task<Secret> CreateApiSecretAsync(CreateSecretRequest request, string apiKey = null, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoRequestWithJsonContentAsync<Secret>(
            HttpMethod.Post,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets"),
            request,
            AuthType.Basic
        );

    [Obsolete("Use SubAccountsClient instead.")]
    public SubAccount CreateSubAccount(CreateSubAccountRequest request, string apiKey = null,
        Credentials creds = null)
    {
        var credentials = creds ?? this.Credentials;
        var accountId = apiKey ?? credentials.ApiKey;
        return new ApiRequest(credentials, this.configuration).DoRequestWithJsonContent<SubAccount>(
            HttpMethod.Post,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{accountId}/subaccounts"),
            request,
            AuthType.Basic
        );
    }

    [Obsolete("Use SubAccountsClient instead.")]
    public Task<SubAccount> CreateSubAccountAsync(CreateSubAccountRequest request, string apiKey = null,
        Credentials creds = null)
    {
        var credentials = creds ?? this.Credentials;
        var accountId = apiKey ?? credentials.ApiKey;
        return new ApiRequest(credentials, this.configuration).DoRequestWithJsonContentAsync<SubAccount>(
            HttpMethod.Post,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{accountId}/subaccounts"),
            request,
            AuthType.Basic
        );
    }

    public Balance GetAccountBalance(Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoGetRequestWithQueryParameters<Balance>(
            ApiRequest.GetBaseUriFor("/account/get-balance"),
            AuthType.Query);

    public Task<Balance> GetAccountBalanceAsync(Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoGetRequestWithQueryParametersAsync<Balance>(
            ApiRequest.GetBaseUriFor("/account/get-balance"),
            AuthType.Query);

    public Secret RetrieveApiSecret(string secretId, string apiKey = null, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoGetRequestWithQueryParameters<Secret>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets/{secretId}"),
            AuthType.Basic
        );

    public Task<Secret> RetrieveApiSecretAsync(string secretId, string apiKey = null, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoGetRequestWithQueryParametersAsync<Secret>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets/{secretId}"),
            AuthType.Basic
        );

    public SecretsRequestResult RetrieveApiSecrets(string apiKey = null, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoGetRequestWithQueryParameters<SecretsRequestResult>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets"),
            AuthType.Basic
        );

    public Task<SecretsRequestResult> RetrieveApiSecretsAsync(string apiKey = null, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoGetRequestWithQueryParametersAsync<SecretsRequestResult>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets"),
            AuthType.Basic
        );

    [Obsolete("Use SubAccountsClient instead.")]
    public SubAccount RetrieveSubAccount(string subAccountKey, string apiKey = null, Credentials creds = null)
    {
        var credentials = creds ?? this.Credentials;
        var accountId = apiKey ?? credentials.ApiKey;
        return new ApiRequest(credentials, this.configuration).DoGetRequestWithQueryParameters<SubAccount>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{accountId}/subaccounts/{subAccountKey}"),
            AuthType.Basic
        );
    }

    [Obsolete("Use SubAccountsClient instead.")]
    public Task<SubAccount> RetrieveSubAccountAsync(string subAccountKey, string apiKey = null,
        Credentials creds = null)
    {
        var credentials = creds ?? this.Credentials;
        var accountId = apiKey ?? credentials.ApiKey;
        return new ApiRequest(credentials, this.configuration).DoGetRequestWithQueryParametersAsync<SubAccount>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{accountId}/subaccounts/{subAccountKey}"),
            AuthType.Basic
        );
    }

    public bool RevokeApiSecret(string secretId, string apiKey = null, Credentials creds = null)
    {
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoDeleteRequestWithUrlContent(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets/{secretId}"),
            null,
            AuthType.Basic
        );
        return true;
    }

    public async Task<bool> RevokeApiSecretAsync(string secretId, string apiKey = null, Credentials creds = null)
    {
        await new ApiRequest(creds ?? this.Credentials, this.configuration).DoDeleteRequestWithUrlContentAsync(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets/{secretId}"),
            null,
            AuthType.Basic
        );
        return true;
    }

    public TopUpResult TopUpAccountBalance(TopUpRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoGetRequestWithQueryParameters<TopUpResult>(
            ApiRequest.GetBaseUriFor("/account/top-up"),
            AuthType.Query,
            request
        );

    public Task<TopUpResult> TopUpAccountBalanceAsync(TopUpRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoGetRequestWithQueryParametersAsync<TopUpResult>(
            ApiRequest.GetBaseUriFor("/account/top-up"),
            AuthType.Query,
            request
        );
}