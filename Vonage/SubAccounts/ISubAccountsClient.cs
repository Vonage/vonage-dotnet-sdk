using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.SubAccounts.GetSubAccount;
using Vonage.SubAccounts.GetSubAccounts;

namespace Vonage.SubAccounts;

/// <summary>
///     Exposes Subaccounts features.
/// </summary>
public interface ISubAccountsClient
{
    /// <summary>
    ///     Retrieve a subaccount of the primary account
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<Result<Account>> GetSubAccount(Result<GetSubAccountRequest> request);

    /// <summary>
    ///     Retrieve all subaccounts of the primary account.
    /// </summary>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<GetSubAccountsResponse>> GetSubAccounts();
}