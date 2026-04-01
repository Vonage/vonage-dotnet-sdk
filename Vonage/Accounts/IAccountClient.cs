using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Accounts;

/// <summary>
///     Exposes methods for managing your Vonage account, including retrieving balance, managing API secrets, and configuring account settings.
/// </summary>
public interface IAccountClient
{
    /// <summary>
    ///     Retrieves the current balance of your Vonage API account.
    /// </summary>
    /// <param name="creds">Optional credentials to override the default credentials.</param>
    /// <returns>A <see cref="Balance"/> object containing the account balance in EUR and auto-reload status.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var balance = await client.AccountClient.GetAccountBalanceAsync();
    /// Console.WriteLine($"Balance: {balance.Value} EUR");
    /// Console.WriteLine($"Auto-reload: {balance.AutoReload}");
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Accounts/GetBalance.cs">Code snippet</seealso>
    Task<Balance> GetAccountBalanceAsync(Credentials creds = null);

    /// <summary>
    ///     Tops up your account balance when auto-reload is enabled in the dashboard.
    ///     The top-up amount matches the amount configured when auto-reload was enabled.
    ///     Account balance is checked every 5-10 minutes for automatic top-up; use this endpoint
    ///     when credit may be exhausted faster than automatic top-up occurs.
    /// </summary>
    /// <param name="request">The top-up request containing the transaction reference from when auto-reload was enabled.</param>
    /// <param name="creds">Optional credentials to override the default credentials.</param>
    /// <returns>A <see cref="TopUpResult"/> containing the response status.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new TopUpRequest { Trx = "your-transaction-reference" };
    /// var result = await client.AccountClient.TopUpAccountBalanceAsync(request);
    /// ]]></code>
    /// </example>
    Task<TopUpResult> TopUpAccountBalanceAsync(TopUpRequest request, Credentials creds = null);

    /// <summary>
    ///     Updates the default webhook callback URLs associated with your account for incoming SMS messages
    ///     and delivery receipts. The provided URLs must be valid and return a 200 OK response for Vonage to save the settings.
    /// </summary>
    /// <param name="request">The request containing the new callback URLs. Use <see cref="AccountSettingsRequest"/>.</param>
    /// <param name="creds">Optional credentials to override the default credentials.</param>
    /// <returns>An <see cref="AccountSettingsResult"/> containing the updated settings and rate limits.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new AccountSettingsRequest
    /// {
    ///     MoCallBackUrl = "https://example.com/webhooks/inbound-sms",
    ///     DrCallBackUrl = "https://example.com/webhooks/delivery-receipt"
    /// };
    /// var result = await client.AccountClient.ChangeAccountSettingsAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Accounts/ChangeAccountSettings.cs">Code snippet</seealso>
    Task<AccountSettingsResult> ChangeAccountSettingsAsync(AccountSettingsRequest request, Credentials creds = null);

    /// <summary>
    ///     Retrieves all API secrets associated with the specified API key.
    ///     It is recommended to rotate secrets periodically for security purposes.
    ///     To manage secrets for secondary accounts, authenticate with primary credentials
    ///     and supply the secondary API key.
    /// </summary>
    /// <param name="apiKey">The API key to retrieve secrets for. Defaults to the authenticated account's API key.</param>
    /// <param name="creds">Optional credentials to override the default credentials.</param>
    /// <returns>A <see cref="SecretsRequestResult"/> containing the list of secrets.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var result = await client.AccountClient.RetrieveApiSecretsAsync();
    /// foreach (var secret in result.Embedded.Secrets)
    /// {
    ///     Console.WriteLine($"Secret ID: {secret.Id}, Created: {secret.CreatedAt}");
    /// }
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Accounts/ListAllSecrets.cs">Code snippet</seealso>
    Task<SecretsRequestResult> RetrieveApiSecretsAsync(string apiKey = null, Credentials creds = null);

    /// <summary>
    ///     Creates a new API secret for the specified API key. You can have a maximum of two secrets per API key.
    /// </summary>
    /// <param name="request">The request containing the new secret value. See <see cref="CreateSecretRequest"/> for password requirements.</param>
    /// <param name="apiKey">The API key to create a secret for. Defaults to the authenticated account's API key.</param>
    /// <param name="creds">Optional credentials to override the default credentials.</param>
    /// <returns>A <see cref="Secret"/> containing the created secret's ID and creation timestamp.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new CreateSecretRequest { Secret = "MyNewS3cret!" };
    /// var secret = await client.AccountClient.CreateApiSecretAsync(request);
    /// Console.WriteLine($"Created secret with ID: {secret.Id}");
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Accounts/CreateSecret.cs">Code snippet</seealso>
    Task<Secret> CreateApiSecretAsync(CreateSecretRequest request, string apiKey = null, Credentials creds = null);

    /// <summary>
    ///     Retrieves information about a specific API secret.
    /// </summary>
    /// <param name="secretId">The unique identifier of the secret to retrieve.</param>
    /// <param name="apiKey">The API key the secret belongs to. Defaults to the authenticated account's API key.</param>
    /// <param name="creds">Optional credentials to override the default credentials.</param>
    /// <returns>A <see cref="Secret"/> containing the secret's ID, creation timestamp, and reference links.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var secret = await client.AccountClient.RetrieveApiSecretAsync("secret-id-123");
    /// Console.WriteLine($"Secret created at: {secret.CreatedAt}");
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Accounts/FetchSecret.cs">Code snippet</seealso>
    Task<Secret> RetrieveApiSecretAsync(string secretId, string apiKey = null, Credentials creds = null);

    /// <summary>
    ///     Revokes (deletes) an API secret. Ensure you have another valid secret before revoking,
    ///     as at least one secret is required for API authentication.
    /// </summary>
    /// <param name="secretId">The unique identifier of the secret to revoke.</param>
    /// <param name="apiKey">The API key the secret belongs to. Defaults to the authenticated account's API key.</param>
    /// <param name="creds">Optional credentials to override the default credentials.</param>
    /// <returns><c>true</c> if the secret was successfully revoked.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var success = await client.AccountClient.RevokeApiSecretAsync("secret-id-123");
    /// if (success)
    /// {
    ///     Console.WriteLine("Secret revoked successfully.");
    /// }
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Accounts/RevokeSecret.cs">Code snippet</seealso>
    Task<bool> RevokeApiSecretAsync(string secretId, string apiKey = null, Credentials creds = null);
}