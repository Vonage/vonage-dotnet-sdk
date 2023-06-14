﻿using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.SubAccounts.CreateSubAccount;
using Vonage.SubAccounts.GetSubAccount;
using Vonage.SubAccounts.GetSubAccounts;

namespace Vonage.SubAccounts;

/// <summary>
///     Exposes Subaccounts features.
/// </summary>
public interface ISubAccountsClient
{
    /// <summary>
    /// Creates a subaccount.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<Account>> CreateSubAccount(Result<CreateSubAccountRequest> request);

    /// <summary>
    ///     Retrieves a subaccount of the primary account
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<Account>> GetSubAccount(Result<GetSubAccountRequest> request);

    /// <summary>
    ///     Retrieves all subaccounts of the primary account.
    /// </summary>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<GetSubAccountsResponse>> GetSubAccounts();
}