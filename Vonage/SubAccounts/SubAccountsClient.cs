using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.SubAccounts.CreateSubAccount;
using Vonage.SubAccounts.GetSubAccount;
using Vonage.SubAccounts.GetSubAccounts;

namespace Vonage.SubAccounts;

/// <inheritdoc />
public class SubAccountsClient : ISubAccountsClient
{
    private readonly string apiKey;
    private readonly VonageHttpClient vonageClient;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="configuration">The client configuration.</param>
    /// <param name="apiKey">The account Id.</param>
    public SubAccountsClient(VonageHttpClientConfiguration configuration, string apiKey)
    {
        this.vonageClient = new VonageHttpClient(configuration, JsonSerializer.BuildWithSnakeCase());
        this.apiKey = apiKey;
    }

    /// <inheritdoc />
    public Task<Result<Account>> CreateSubAccount(Result<CreateSubAccountRequest> request) =>
        request.Map(incompleteRequest => incompleteRequest.WithApiKey(this.apiKey))
            .BindAsync(completeRequest =>
                this.vonageClient.SendWithResponseAsync<CreateSubAccountRequest, Account>(completeRequest));

    /// <inheritdoc />
    public Task<Result<Account>> GetSubAccount(Result<GetSubAccountRequest> request) =>
        request.Map(incompleteRequest => incompleteRequest.WithApiKey(this.apiKey))
            .BindAsync(completeRequest =>
                this.vonageClient.SendWithResponseAsync<GetSubAccountRequest, Account>(completeRequest));

    /// <inheritdoc />
    public async Task<Result<GetSubAccountsResponse>> GetSubAccounts() =>
        await this.vonageClient
            .SendWithResponseAsync<GetSubAccountsRequest, EmbeddedResponse<GetSubAccountsResponse>>(
                GetSubAccountsRequest.Build(this.apiKey))
            .Map(value => value.Content);
}