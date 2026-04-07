using System.Threading.Tasks;
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
///     Exposes Subaccounts API features for creating and managing subaccounts under a primary account, enabling
///     differential product configuration, reporting, and billing.
/// </summary>
public interface ISubAccountsClient
{
    /// <summary>
    ///     Creates a subaccount under the primary account.
    /// </summary>
    /// <param name="request">The request containing the subaccount details. See <see cref="CreateSubAccountRequest"/>.</param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing the created <see cref="Account"/> with its API key and secret on success,
    ///     or an error on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = CreateSubAccountRequest.Build()
    ///     .WithName("Department A")
    ///     .DisableSharedAccountBalance()
    ///     .Create();
    /// var result = await client.SubAccountsClient.CreateSubAccountAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/SubAccounts">More examples in the snippets repository</seealso>
    Task<Result<Account>> CreateSubAccountAsync(Result<CreateSubAccountRequest> request);

    /// <summary>
    ///     Retrieves a list of balance transfers that have taken place for the primary account within a specified time period.
    /// </summary>
    /// <param name="request">The request containing the time period filter. See <see cref="GetTransfersRequest"/>.</param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing an array of <see cref="Transfer"/> records on success,
    ///     or an error on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = GetTransfersRequest.Build()
    ///     .WithStartDate(DateTimeOffset.UtcNow.AddDays(-30))
    ///     .Create();
    /// var result = await client.SubAccountsClient.GetBalanceTransfersAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/SubAccounts">More examples in the snippets repository</seealso>
    Task<Result<Transfer[]>> GetBalanceTransfersAsync(Result<GetTransfersRequest> request);

    /// <summary>
    ///     Retrieves a list of credit transfers that have taken place for the primary account within a specified time period.
    /// </summary>
    /// <param name="request">The request containing the time period filter. See <see cref="GetTransfersRequest"/>.</param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing an array of <see cref="Transfer"/> records on success,
    ///     or an error on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = GetTransfersRequest.Build()
    ///     .WithStartDate(DateTimeOffset.UtcNow.AddDays(-30))
    ///     .Create();
    /// var result = await client.SubAccountsClient.GetCreditTransfersAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/SubAccounts">More examples in the snippets repository</seealso>
    Task<Result<Transfer[]>> GetCreditTransfersAsync(Result<GetTransfersRequest> request);

    /// <summary>
    ///     Retrieves a specific subaccount by its API key.
    /// </summary>
    /// <param name="request">The request containing the subaccount API key. See <see cref="GetSubAccountRequest"/>.</param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing the <see cref="Account"/> details on success,
    ///     or an error on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = GetSubAccountRequest.Parse("bbe6222f");
    /// var result = await client.SubAccountsClient.GetSubAccountAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/SubAccounts">More examples in the snippets repository</seealso>
    Task<Result<Account>> GetSubAccountAsync(Result<GetSubAccountRequest> request);

    /// <summary>
    ///     Retrieves the primary account and all its subaccounts.
    /// </summary>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing a <see cref="GetSubAccountsResponse"/> with the primary account
    ///     and all subaccounts on success, or an error on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var result = await client.SubAccountsClient.GetSubAccountsAsync();
    /// result.IfSuccess(response =>
    /// {
    ///     Console.WriteLine($"Primary: {response.PrimaryAccount.Name}");
    ///     foreach (var sub in response.SubAccounts)
    ///         Console.WriteLine($"Sub: {sub.Name}");
    /// });
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/SubAccounts">More examples in the snippets repository</seealso>
    Task<Result<GetSubAccountsResponse>> GetSubAccountsAsync();

    /// <summary>
    ///     Transfers balance between the primary account and one of its subaccounts.
    /// </summary>
    /// <param name="request">The request containing transfer details. See <see cref="TransferAmountRequest"/>.</param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing the <see cref="Transfer"/> record on success,
    ///     or an error on failure.
    /// </returns>
    /// <remarks>
    ///     The balance available for transfer equals the absolute value of (account_balance - credit_limit) of the source account.
    /// </remarks>
    /// <example>
    /// <code><![CDATA[
    /// var request = TransferAmountRequest.Build()
    ///     .WithFrom("7c9738e6")
    ///     .WithTo("ad6dc56f")
    ///     .WithAmount(123.45m)
    ///     .Create();
    /// var result = await client.SubAccountsClient.TransferBalanceAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/SubAccounts">More examples in the snippets repository</seealso>
    Task<Result<Transfer>> TransferBalanceAsync(Result<TransferAmountRequest> request);

    /// <summary>
    ///     Transfers credit limit between the primary account and one of its subaccounts.
    /// </summary>
    /// <param name="request">The request containing transfer details. See <see cref="TransferAmountRequest"/>.</param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing the <see cref="Transfer"/> record on success,
    ///     or an error on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = TransferAmountRequest.Build()
    ///     .WithFrom("7c9738e6")
    ///     .WithTo("ad6dc56f")
    ///     .WithAmount(100.00m)
    ///     .WithReference("Monthly credit allocation")
    ///     .Create();
    /// var result = await client.SubAccountsClient.TransferCreditAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/SubAccounts">More examples in the snippets repository</seealso>
    Task<Result<Transfer>> TransferCreditAsync(Result<TransferAmountRequest> request);

    /// <summary>
    ///     Transfers a phone number from one account to another within the primary account's hierarchy.
    /// </summary>
    /// <param name="request">The request containing transfer details. See <see cref="TransferNumberRequest"/>.</param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing the <see cref="TransferNumberResponse"/> on success,
    ///     or an error on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = TransferNumberRequest.Build()
    ///     .WithFrom("7c9738e6")
    ///     .WithTo("ad6dc56f")
    ///     .WithNumber("447700900000")
    ///     .WithCountry("GB")
    ///     .Create();
    /// var result = await client.SubAccountsClient.TransferNumberAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/SubAccounts">More examples in the snippets repository</seealso>
    Task<Result<TransferNumberResponse>> TransferNumberAsync(Result<TransferNumberRequest> request);

    /// <summary>
    ///     Updates the properties of an existing subaccount.
    /// </summary>
    /// <param name="request">The request containing the properties to update. See <see cref="UpdateSubAccountRequest"/>.</param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing the updated <see cref="Account"/> on success,
    ///     or an error on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = UpdateSubAccountRequest.Build()
    ///     .WithSubAccountKey("bbe6222f")
    ///     .WithName("Department B")
    ///     .SuspendAccount()
    ///     .Create();
    /// var result = await client.SubAccountsClient.UpdateSubAccountAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/SubAccounts">More examples in the snippets repository</seealso>
    Task<Result<Account>> UpdateSubAccountAsync(Result<UpdateSubAccountRequest> request);
}