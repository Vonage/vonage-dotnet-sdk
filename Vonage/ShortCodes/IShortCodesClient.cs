using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.ShortCodes;

/// <summary>
///     Exposes US Short Codes API features for sending event-based alerts and two-factor authentication messages via
///     pre-approved short codes in the United States.
/// </summary>
public interface IShortCodesClient
{
    /// <summary>
    ///     Queries the list of phone numbers that have opted in to receive messages from your short code.
    /// </summary>
    /// <param name="request">The query parameters for pagination. See <see cref="OptInQueryRequest"/>.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns>An <see cref="OptInSearchResponse"/> containing the list of opted-in phone numbers.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new OptInQueryRequest { Page = "1", PageSize = "10" };
    /// var response = await client.ShortCodesClient.QueryOptInsAsync(request);
    /// foreach (var record in response.OptInList)
    /// {
    ///     Console.WriteLine($"MSISDN: {record.Msisdn}, Opted In: {record.OptIn}");
    /// }
    /// ]]></code>
    /// </example>
    Task<OptInSearchResponse> QueryOptInsAsync(OptInQueryRequest request, Credentials creds = null);

    /// <summary>
    ///     Manages the opt-in status for a specific phone number, allowing it to receive messages from your short code.
    /// </summary>
    /// <param name="request">The request containing the phone number to manage. See <see cref="OptInManageRequest"/>.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns>An <see cref="OptInRecord"/> containing the updated opt-in status for the phone number.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new OptInManageRequest { Msisdn = "14155551234" };
    /// var record = await client.ShortCodesClient.ManageOptInAsync(request);
    /// Console.WriteLine($"Opt-in status: {record.OptIn}");
    /// ]]></code>
    /// </example>
    Task<OptInRecord> ManageOptInAsync(OptInManageRequest request, Credentials creds = null);

    /// <summary>
    ///     Sends an event-based alert message to a phone number using your pre-approved short code template.
    /// </summary>
    /// <param name="request">The alert message details including recipient and template. See <see cref="AlertRequest"/>.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns>An <see cref="AlertResponse"/> containing the message delivery status.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new AlertRequest
    /// {
    ///     To = "14155551234",
    ///     Template = "Your order #{order_id} has shipped!"
    /// };
    /// var response = await client.ShortCodesClient.SendAlertAsync(request);
    /// ]]></code>
    /// </example>
    Task<AlertResponse> SendAlertAsync(AlertRequest request, Credentials creds = null);

    /// <summary>
    ///     Sends a two-factor authentication PIN code to a phone number using your pre-approved short code.
    /// </summary>
    /// <param name="request">The two-factor authentication request details. See <see cref="TwoFactorAuthRequest"/>.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns>A <see cref="TwoFactorAuthResponse"/> containing the message delivery status.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new TwoFactorAuthRequest();
    /// var response = await client.ShortCodesClient.SendTwoFactorAuthAsync(request);
    /// ]]></code>
    /// </example>
    Task<TwoFactorAuthResponse> SendTwoFactorAuthAsync(TwoFactorAuthRequest request, Credentials creds = null);
}