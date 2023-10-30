using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Request;

namespace Vonage.Accounts;

public class AccountClient : IAccountClient
{
    private readonly Configuration configuration;
    private readonly ITimeProvider timeProvider = new TimeProvider();
    public Credentials Credentials { get; set; }

    public AccountClient(Credentials creds = null)
    {
        this.Credentials = creds;
        this.configuration = Configuration.Instance;
    }

    internal AccountClient(Credentials creds, Configuration configuration, ITimeProvider timeProvider)
    {
        this.Credentials = creds;
        this.configuration = configuration;
        this.timeProvider = timeProvider;
    }

    /// <inheritdoc/>
    public Task<AccountSettingsResult> ChangeAccountSettingsAsync(AccountSettingsRequest request,
        Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObjectAsync<AccountSettingsResult>
            (
                ApiRequest.GetBaseUriFor("/account/settings"),
                request
            );

    /// <inheritdoc/>
    public Task<Secret> CreateApiSecretAsync(CreateSecretRequest request, string apiKey = null,
        Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContentAsync<Secret>(
                HttpMethod.Post,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets"),
                request,
                AuthType.Basic
            );

    /// <inheritdoc/>
    public Task<Balance> GetAccountBalanceAsync(Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<Balance>(
                ApiRequest.GetBaseUriFor("/account/get-balance"),
                AuthType.Query);

    /// <inheritdoc/>
    public Task<Secret> RetrieveApiSecretAsync(string secretId, string apiKey = null, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<Secret>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets/{secretId}"),
                AuthType.Basic
            );

    /// <inheritdoc/>
    public Task<SecretsRequestResult> RetrieveApiSecretsAsync(string apiKey = null, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<SecretsRequestResult>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets"),
                AuthType.Basic
            );

    /// <inheritdoc/>
    public async Task<bool> RevokeApiSecretAsync(string secretId, string apiKey = null, Credentials creds = null)
    {
        await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoDeleteRequestWithUrlContentAsync(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/secrets/{secretId}"),
                null,
                AuthType.Basic
            );
        return true;
    }

    /// <inheritdoc/>
    public Task<TopUpResult> TopUpAccountBalanceAsync(TopUpRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<TopUpResult>(
                ApiRequest.GetBaseUriFor("/account/top-up"),
                AuthType.Query,
                request
            );

    private Credentials GetCredentials(Credentials overridenCredentials) => overridenCredentials ?? this.Credentials;
}