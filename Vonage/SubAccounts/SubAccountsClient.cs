using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.SubAccounts.CreateSubAccount;
using Vonage.SubAccounts.GetCreditTransfers;
using Vonage.SubAccounts.GetSubAccount;
using Vonage.SubAccounts.GetSubAccounts;
using Vonage.SubAccounts.Transfer;
using Vonage.SubAccounts.TransferNumber;
using Vonage.SubAccounts.UpdateSubAccount;

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
    public Task<Result<Account>> CreateSubAccountAsync(Result<CreateSubAccountRequest> request) =>
        request.Map(incompleteRequest => incompleteRequest.WithApiKey(this.apiKey))
            .BindAsync(completeRequest =>
                this.vonageClient.SendWithResponseAsync<CreateSubAccountRequest, Account>(completeRequest));

    /// <inheritdoc />
    public Task<Result<CreditTransfer[]>> GetCreditTransfersAsync(Result<GetCreditTransfersRequest> request) =>
        request.Map(incompleteRequest => incompleteRequest.WithApiKey(this.apiKey))
            .BindAsync(completeRequest =>
                this.vonageClient
                    .SendWithResponseAsync<GetCreditTransfersRequest, EmbeddedResponse<GetCreditTransfersResponse>>(
                        completeRequest))
            .Map(value => value.Content.CreditTransfers);

    /// <inheritdoc />
    public Task<Result<Account>> GetSubAccountAsync(Result<GetSubAccountRequest> request) =>
        request.Map(incompleteRequest => incompleteRequest.WithApiKey(this.apiKey))
            .BindAsync(completeRequest =>
                this.vonageClient.SendWithResponseAsync<GetSubAccountRequest, Account>(completeRequest));

    /// <inheritdoc />
    public async Task<Result<GetSubAccountsResponse>> GetSubAccountsAsync() =>
        await this.vonageClient
            .SendWithResponseAsync<GetSubAccountsRequest, EmbeddedResponse<GetSubAccountsResponse>>(
                GetSubAccountsRequest.Build(this.apiKey))
            .Map(value => value.Content);

    /// <inheritdoc />
    public Task<Result<BalanceTransfer>> TransferBalanceAsync(Result<TransferRequest> request) =>
        request
            .Map(incompleteRequest => incompleteRequest.WithApiKey(this.apiKey))
            .Map(incompleteRequest => incompleteRequest.WithEndpoint(TransferRequest.BalanceTransfer))
            .BindAsync(completeRequest =>
                this.vonageClient.SendWithResponseAsync<TransferRequest, BalanceTransfer>(completeRequest));

    /// <inheritdoc />
    public Task<Result<CreditTransfer>> TransferCreditAsync(Result<TransferRequest> request) =>
        request
            .Map(incompleteRequest => incompleteRequest.WithApiKey(this.apiKey))
            .Map(incompleteRequest => incompleteRequest.WithEndpoint(TransferRequest.CreditTransfer))
            .BindAsync(completeRequest =>
                this.vonageClient.SendWithResponseAsync<TransferRequest, CreditTransfer>(completeRequest));

    /// <inheritdoc />
    public Task<Result<TransferNumberResponse>> TransferNumberAsync(Result<TransferNumberRequest> request) =>
        request.Map(incompleteRequest => incompleteRequest.WithApiKey(this.apiKey))
            .BindAsync(completeRequest =>
                this.vonageClient
                    .SendWithResponseAsync<TransferNumberRequest, TransferNumberResponse>(completeRequest));

    /// <inheritdoc />
    public Task<Result<Account>> UpdateSubAccountAsync(Result<UpdateSubAccountRequest> request) =>
        request.Map(incompleteRequest => incompleteRequest.WithApiKey(this.apiKey))
            .BindAsync(completeRequest =>
                this.vonageClient.SendWithResponseAsync<UpdateSubAccountRequest, Account>(completeRequest));

    /// <inheritdoc />
    public Task<Result<TransferNumberResponse>> UpdateSubAccountAsync(Result<TransferNumberRequest> request) =>
        request.Map(incompleteRequest => incompleteRequest.WithApiKey(this.apiKey))
            .BindAsync(completeRequest =>
                this.vonageClient
                    .SendWithResponseAsync<TransferNumberRequest, TransferNumberResponse>(completeRequest));
}