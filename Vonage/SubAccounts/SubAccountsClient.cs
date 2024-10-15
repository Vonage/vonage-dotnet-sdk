#region
using System;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.SubAccounts.CreateSubAccount;
using Vonage.SubAccounts.GetSubAccount;
using Vonage.SubAccounts.GetSubAccounts;
using Vonage.SubAccounts.GetTransfers;
using Vonage.SubAccounts.TransferAmount;
using Vonage.SubAccounts.TransferNumber;
using Vonage.SubAccounts.UpdateSubAccount;
#endregion

namespace Vonage.SubAccounts;

/// <inheritdoc />
public class SubAccountsClient : ISubAccountsClient
{
    private readonly string apiKey;

    private readonly TransferMapping balanceTransferMapping =
        new(GetTransfersRequest.BalanceTransfer, TransferAmountRequest.BalanceTransfer,
            response => response.BalanceTransfers);

    private readonly TransferMapping creditTransferMapping =
        new(GetTransfersRequest.CreditTransfer, TransferAmountRequest.CreditTransfer,
            response => response.CreditTransfers);

    private readonly VonageHttpClient<StandardApiError> vonageClient;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="configuration">The client configuration.</param>
    /// <param name="apiKey">The account Id.</param>
    public SubAccountsClient(VonageHttpClientConfiguration configuration, string apiKey)
    {
        this.vonageClient =
            new VonageHttpClient<StandardApiError>(configuration, JsonSerializerBuilder.BuildWithSnakeCase());
        this.apiKey = apiKey;
    }

    /// <inheritdoc />
    public Task<Result<Account>> CreateSubAccountAsync(Result<CreateSubAccountRequest> request) =>
        request.Map(incompleteRequest => incompleteRequest.WithApiKey(this.apiKey))
            .BindAsync(completeRequest =>
                this.vonageClient.SendWithResponseAsync<CreateSubAccountRequest, Account>(completeRequest));

    /// <inheritdoc />
    public Task<Result<Transfer[]>> GetBalanceTransfersAsync(Result<GetTransfersRequest> request) =>
        this.MapTransfersAsync(request, this.balanceTransferMapping);

    /// <inheritdoc />
    public Task<Result<Transfer[]>> GetCreditTransfersAsync(Result<GetTransfersRequest> request) =>
        this.MapTransfersAsync(request, this.creditTransferMapping);

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
            .Map(value => value.Content).ConfigureAwait(false);

    /// <inheritdoc />
    public Task<Result<Transfer>> TransferBalanceAsync(Result<TransferAmountRequest> request) =>
        this.MapTransfersAsync(request, this.balanceTransferMapping);

    /// <inheritdoc />
    public Task<Result<Transfer>> TransferCreditAsync(Result<TransferAmountRequest> request) =>
        this.MapTransfersAsync(request, this.creditTransferMapping);

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

    private Task<Result<Transfer[]>> MapTransfersAsync(Result<GetTransfersRequest> request, TransferMapping mapping) =>
        request.Map(incompleteRequest => incompleteRequest.WithApiKey(this.apiKey))
            .Map(incompleteRequest => incompleteRequest.WithEndpoint(mapping.GetEndpointKey))
            .BindAsync(completeRequest =>
                this.vonageClient.SendWithResponseAsync<GetTransfersRequest, EmbeddedResponse<GetTransfersResponse>>(
                    completeRequest))
            .Map(value => mapping.GetMapping(value.Content));

    private Task<Result<Transfer>> MapTransfersAsync(Result<TransferAmountRequest> request, TransferMapping mapping) =>
        request.Map(incompleteRequest => incompleteRequest.WithApiKey(this.apiKey))
            .Map(incompleteRequest => incompleteRequest.WithEndpoint(mapping.UpdateEndpointKey))
            .BindAsync(completeRequest =>
                this.vonageClient.SendWithResponseAsync<TransferAmountRequest, Transfer>(completeRequest));

    private sealed record TransferMapping(
        string GetEndpointKey,
        string UpdateEndpointKey,
        Func<GetTransfersResponse, Transfer[]> GetMapping);
}