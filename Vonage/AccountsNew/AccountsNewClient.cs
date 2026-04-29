using System.Threading.Tasks;
using Vonage.AccountsNew.ChangeAccountSettings;
using Vonage.AccountsNew.CreateSecret;
using Vonage.AccountsNew.GetBalance;
using Vonage.AccountsNew.GetSecret;
using Vonage.AccountsNew.GetSecrets;
using Vonage.AccountsNew.RevokeSecret;
using Vonage.AccountsNew.TopUpBalance;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;

namespace Vonage.AccountsNew;

/// <inheritdoc />
internal class AccountsNewClient : IAccountsNewClient
{
    private readonly VonageHttpClient<StandardApiError> vonageClient;

    /// <summary>
    ///     Creates a new Accounts API client.
    /// </summary>
    /// <param name="configuration">The HTTP client configuration.</param>
    public AccountsNewClient(VonageHttpClientConfiguration configuration) =>
        this.vonageClient =
            new VonageHttpClient<StandardApiError>(configuration, JsonSerializerBuilder.BuildWithSnakeCase());

    /// <inheritdoc />
    public Task<Result<GetBalanceResponse>> GetBalanceAsync() =>
        this.vonageClient.SendWithResponseAsync<GetBalanceRequest, GetBalanceResponse>(GetBalanceRequest.Default);

    /// <inheritdoc />
    public Task<Result<TopUpBalanceResponse>> TopUpBalanceAsync(Result<TopUpBalanceRequest> request) =>
        this.vonageClient.SendWithResponseAsync<TopUpBalanceRequest, TopUpBalanceResponse>(request);

    /// <inheritdoc />
    public Task<Result<ChangeAccountSettingsResponse>> ChangeAccountSettingsAsync(
        Result<ChangeAccountSettingsRequest> request) =>
        this.vonageClient.SendWithResponseAsync<ChangeAccountSettingsRequest, ChangeAccountSettingsResponse>(request);

    /// <inheritdoc />
    public Task<Result<GetSecretsResponse>> GetSecretsAsync(Result<GetSecretsRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetSecretsRequest, GetSecretsResponse>(request);

    /// <inheritdoc />
    public Task<Result<SecretInfo>> CreateSecretAsync(Result<CreateSecretRequest> request) =>
        this.vonageClient.SendWithResponseAsync<CreateSecretRequest, SecretInfo>(request);

    /// <inheritdoc />
    public Task<Result<SecretInfo>> GetSecretAsync(Result<GetSecretRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetSecretRequest, SecretInfo>(request);

    /// <inheritdoc />
    public Task<Result<Unit>> RevokeSecretAsync(Result<RevokeSecretRequest> request) =>
        this.vonageClient.SendAsync(request);
}
