﻿using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.SubAccounts.CreateSubAccount;
using Vonage.SubAccounts.GetSubAccount;
using Vonage.SubAccounts.GetSubAccounts;
using Vonage.SubAccounts.GetTransfers;
using Vonage.SubAccounts.TransferAmount;
using Vonage.SubAccounts.TransferNumber;
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
    ///     Retrieve a list of balance transfers that have taken place for a primary account within a specified time period.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<Transfer[]>> GetBalanceTransfersAsync(Result<GetTransfersRequest> request);

    /// <summary>
    ///     Retrieve a list of credit transfers that have taken place for a primary account within a specified time period.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<Transfer[]>> GetCreditTransfersAsync(Result<GetTransfersRequest> request);

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
    ///     Transfer balance limit between a primary account and one of its subaccounts.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<Transfer>> TransferBalanceAsync(Result<TransferAmountRequest> request);

    /// <summary>
    ///     Transfer credit limit between a primary account and one of its subaccounts.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<Transfer>> TransferCreditAsync(Result<TransferAmountRequest> request);

    /// <summary>
    ///     Transfer number from one account to another.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<TransferNumberResponse>> TransferNumberAsync(Result<TransferNumberRequest> request);

    /// <summary>
    ///     Updates a subaccount.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<Account>> UpdateSubAccountAsync(Result<UpdateSubAccountRequest> request);
}