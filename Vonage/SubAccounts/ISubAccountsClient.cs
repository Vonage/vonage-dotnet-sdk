using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.SubAccounts.CreateSubAccount;
using Vonage.SubAccounts.GetCreditTransfers;
using Vonage.SubAccounts.GetSubAccount;
using Vonage.SubAccounts.GetSubAccounts;
using Vonage.SubAccounts.TransferCredit;
using Vonage.SubAccounts.UpdateSubAccount;

namespace Vonage.SubAccounts;

/// <summary>
///     Exposes Subaccounts features.
/// </summary>
public interface ISubAccountsClient
{
    /// <summary>
    ///     Creates a subaccount.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<Account>> CreateSubAccountAsync(Result<CreateSubAccountRequest> request);

    /// <summary>
    ///     Retrieve a list of credit transfers that have taken place for a primary account within a specified time period.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<CreditTransfer[]>> GetCreditTransfersAsync(Result<GetCreditTransfersRequest> request);

    /// <summary>
    ///     Retrieves a subaccount of the primary account
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<Account>> GetSubAccountAsync(Result<GetSubAccountRequest> request);

    /// <summary>
    ///     Retrieves all subaccounts of the primary account.
    /// </summary>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<GetSubAccountsResponse>> GetSubAccountsAsync();

    /// <summary>
    ///     Transfer credit limit between a primary account and one of its subaccounts.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<Result<CreditTransfer>> TransferCreditAsync(Result<TransferCreditRequest> request);

    /// <summary>
    ///     Updates a subaccount.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<Account>> UpdateSubAccountAsync(Result<UpdateSubAccountRequest> request);
}