using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Numbers;

/// <summary>
///     Exposes Numbers API features for managing virtual phone numbers including searching,
///     purchasing, configuring, and transferring numbers between accounts.
/// </summary>
public interface INumbersClient
{
    /// <summary>
    ///     Retrieves all the inbound numbers associated with your Vonage account.
    /// </summary>
    /// <param name="request">The search request containing optional filters. See <see cref="NumberSearchRequest"/>.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns>A <see cref="NumbersSearchResponse"/> containing the list of owned numbers and total count.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new NumberSearchRequest { Country = "US" };
    /// var response = await client.NumbersClient.GetOwnedNumbersAsync(request);
    /// foreach (var number in response.Numbers)
    /// {
    ///     Console.WriteLine($"Number: {number.Msisdn}");
    /// }
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Numbers">More examples in the snippets repository</seealso>
    Task<NumbersSearchResponse> GetOwnedNumbersAsync(NumberSearchRequest request, Credentials creds = null);

    /// <summary>
    ///     Retrieves inbound numbers that are available for purchase in the specified country.
    /// </summary>
    /// <param name="request">The search request containing the country and optional filters. See <see cref="NumberSearchRequest"/>.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns>A <see cref="NumbersSearchResponse"/> containing the list of available numbers and total count.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new NumberSearchRequest { Country = "GB", Type = "mobile-lvn" };
    /// var response = await client.NumbersClient.GetAvailableNumbersAsync(request);
    /// foreach (var number in response.Numbers)
    /// {
    ///     Console.WriteLine($"Available: {number.Msisdn}, Cost: {number.Cost}");
    /// }
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Numbers">More examples in the snippets repository</seealso>
    Task<NumbersSearchResponse> GetAvailableNumbersAsync(NumberSearchRequest request, Credentials creds = null);

    /// <summary>
    ///     Purchases a specific inbound number to add it to your account.
    /// </summary>
    /// <param name="request">The request containing the country and MSISDN to purchase. See <see cref="NumberTransactionRequest"/>.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns>A <see cref="NumberTransactionResponse"/> indicating success or failure with an error code.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new NumberTransactionRequest { Country = "GB", Msisdn = "447700900000" };
    /// var response = await client.NumbersClient.BuyANumberAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Numbers">More examples in the snippets repository</seealso>
    Task<NumberTransactionResponse> BuyANumberAsync(NumberTransactionRequest request, Credentials creds = null);

    /// <summary>
    ///     Cancels your subscription for a specific inbound number, releasing it from your account.
    /// </summary>
    /// <param name="request">The request containing the country and MSISDN to cancel. See <see cref="NumberTransactionRequest"/>.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns>A <see cref="NumberTransactionResponse"/> indicating success or failure with an error code.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new NumberTransactionRequest { Country = "GB", Msisdn = "447700900000" };
    /// var response = await client.NumbersClient.CancelANumberAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Numbers">More examples in the snippets repository</seealso>
    Task<NumberTransactionResponse> CancelANumberAsync(NumberTransactionRequest request, Credentials creds = null);

    /// <summary>
    ///     Updates the configuration of a number you own, including webhook URLs and voice callback settings.
    /// </summary>
    /// <param name="request">The request containing the number and new configuration. See <see cref="UpdateNumberRequest"/>.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns>A <see cref="NumberTransactionResponse"/> indicating success or failure with an error code.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new UpdateNumberRequest
    /// {
    ///     Country = "GB",
    ///     Msisdn = "447700900000",
    ///     AppId = "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"
    /// };
    /// var response = await client.NumbersClient.UpdateANumberAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Numbers">More examples in the snippets repository</seealso>
    Task<NumberTransactionResponse> UpdateANumberAsync(UpdateNumberRequest request, Credentials creds = null);

    /// <summary>
    ///     Transfers a number you own to a subaccount.
    /// </summary>
    /// <param name="request">The request containing the number and transfer details. See <see cref="NumberTransferRequest"/>.</param>
    /// <param name="apiKey">The API key of the account performing the transfer.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns>A <see cref="NumberTransferResponse"/> containing the transfer details.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new NumberTransferRequest
    /// {
    ///     Country = "GB",
    ///     Number = "447700900000",
    ///     From = "master-api-key",
    ///     To = "subaccount-api-key"
    /// };
    /// var response = await client.NumbersClient.TransferANumberAsync(request, "master-api-key");
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Numbers">More examples in the snippets repository</seealso>
    Task<NumberTransferResponse> TransferANumberAsync(NumberTransferRequest request, string apiKey,
        Credentials creds = null);
}