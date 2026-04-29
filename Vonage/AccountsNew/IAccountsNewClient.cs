using System.Threading.Tasks;
using Vonage.AccountsNew.ChangeAccountSettings;
using Vonage.AccountsNew.CreateSecret;
using Vonage.AccountsNew.GetBalance;
using Vonage.AccountsNew.GetSecret;
using Vonage.AccountsNew.GetSecrets;
using Vonage.AccountsNew.RevokeSecret;
using Vonage.AccountsNew.TopUpBalance;
using Vonage.Common.Monads;

namespace Vonage.AccountsNew;

/// <summary>
///     Exposes Vonage Account API features for managing account balance, settings, and API secrets.
/// </summary>
public interface IAccountsNewClient
{
    /// <summary>
    ///     Retrieves the current balance of the Vonage API account.
    /// </summary>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing the <see cref="GetBalanceResponse"/> on success,
    ///     or an error on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var result = await client.AccountsNewClient.GetBalanceAsync();
    /// result.IfSuccess(balance => Console.WriteLine($"Balance: {balance.Value} EUR"));
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Accounts">More examples in the snippets repository</seealso>
    Task<Result<GetBalanceResponse>> GetBalanceAsync();

    /// <summary>
    ///     Tops up the account balance using the auto-reload configuration.
    /// </summary>
    /// <param name="request">The request containing the transaction reference for auto-reload.</param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing the <see cref="TopUpBalanceResponse"/> on success,
    ///     or an error on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = TopUpBalanceRequest.Build()
    ///     .WithTransactionReference("8ef2447e69604f642ae59363aa5f781b")
    ///     .Create();
    /// var result = await client.AccountsNewClient.TopUpBalanceAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Accounts">More examples in the snippets repository</seealso>
    Task<Result<TopUpBalanceResponse>> TopUpBalanceAsync(Result<TopUpBalanceRequest> request);

    /// <summary>
    ///     Updates the default webhook callback URLs and HTTP method for the account.
    /// </summary>
    /// <param name="request">The request containing the new settings to apply.</param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing the updated <see cref="ChangeAccountSettingsResponse"/> on success,
    ///     or an error on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = ChangeAccountSettingsRequest.Build()
    ///     .WithInboundSmsCallbackUrl("https://example.com/webhooks/inbound-sms")
    ///     .WithDeliveryReceiptCallbackUrl("https://example.com/webhooks/delivery-receipt")
    ///     .Create();
    /// var result = await client.AccountsNewClient.ChangeAccountSettingsAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Accounts">More examples in the snippets repository</seealso>
    Task<Result<ChangeAccountSettingsResponse>> ChangeAccountSettingsAsync(
        Result<ChangeAccountSettingsRequest> request);

    /// <summary>
    ///     Retrieves all API secrets for the specified account.
    /// </summary>
    /// <param name="request">The request containing the API key to retrieve secrets for.</param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing the <see cref="GetSecretsResponse"/> on success,
    ///     or an error on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = GetSecretsRequest.Build()
    ///     .WithApiKey("abcd1234")
    ///     .Create();
    /// var result = await client.AccountsNewClient.GetSecretsAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Accounts">More examples in the snippets repository</seealso>
    Task<Result<GetSecretsResponse>> GetSecretsAsync(Result<GetSecretsRequest> request);

    /// <summary>
    ///     Creates a new API secret for the specified account.
    /// </summary>
    /// <param name="request">The request containing the API key and the new secret value.</param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing the created <see cref="SecretInfo"/> on success,
    ///     or an error on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = CreateSecretRequest.Build()
    ///     .WithApiKey("abcd1234")
    ///     .WithSecret("example-4PI-s3cret")
    ///     .Create();
    /// var result = await client.AccountsNewClient.CreateSecretAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Accounts">More examples in the snippets repository</seealso>
    Task<Result<SecretInfo>> CreateSecretAsync(Result<CreateSecretRequest> request);

    /// <summary>
    ///     Retrieves a specific API secret by its identifier.
    /// </summary>
    /// <param name="request">The request containing the API key and secret identifier.</param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing the <see cref="SecretInfo"/> on success,
    ///     or an error on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = GetSecretRequest.Build()
    ///     .WithApiKey("abcd1234")
    ///     .WithSecretId("ad6dc56f-07b5-46e1-a527-85530e625800")
    ///     .Create();
    /// var result = await client.AccountsNewClient.GetSecretAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Accounts">More examples in the snippets repository</seealso>
    Task<Result<SecretInfo>> GetSecretAsync(Result<GetSecretRequest> request);

    /// <summary>
    ///     Revokes (deletes) an API secret. Ensure at least one other valid secret exists before revoking.
    /// </summary>
    /// <param name="request">The request containing the API key and secret identifier to revoke.</param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing <see cref="Common.Monads.Unit"/> on success,
    ///     or an error on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = RevokeSecretRequest.Build()
    ///     .WithApiKey("abcd1234")
    ///     .WithSecretId("ad6dc56f-07b5-46e1-a527-85530e625800")
    ///     .Create();
    /// var result = await client.AccountsNewClient.RevokeSecretAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Accounts">More examples in the snippets repository</seealso>
    Task<Result<Unit>> RevokeSecretAsync(Result<RevokeSecretRequest> request);
}
