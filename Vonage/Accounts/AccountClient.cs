using System.Threading.Tasks;
using Vonage.Accounts.ChangeAccountSettings;
using Vonage.Accounts.CreateSecret;
using Vonage.Accounts.GetBalance;
using Vonage.Accounts.GetSecret;
using Vonage.Accounts.GetSecrets;
using Vonage.Accounts.RevokeSecret;
using Vonage.Accounts.TopUpBalance;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;

namespace Vonage.Accounts;

/// <inheritdoc />
internal class AccountClient : IAccountClient
{
    private readonly VonageHttpClient<StandardApiError> vonageClient;

    /// <summary>
    ///     Creates a new Accounts API client.
    /// </summary>
    /// <param name="configuration">The HTTP client configuration.</param>
    public AccountClient(VonageHttpClientConfiguration configuration) =>
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
