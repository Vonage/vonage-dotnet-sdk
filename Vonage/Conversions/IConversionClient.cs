using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Conversions;

/// <summary>
///     Provides methods for submitting conversion data to the Vonage Conversion API.
///     Conversion tracking helps Vonage improve message routing by reporting whether your users
///     completed a desired action (e.g., replied to an SMS, answered a call, clicked a link).
/// </summary>
public interface IConversionClient
{
    /// <summary>
    ///     Reports a conversion event for an SMS message.
    ///     Use this to inform Vonage whether the recipient completed your call-to-action
    ///     after receiving an SMS. This data helps improve message delivery routing.
    /// </summary>
    /// <param name="request">The conversion request containing the message ID and conversion status.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns><c>true</c> if the conversion was successfully reported.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new ConversionRequest
    /// {
    ///     MessageId = "0A0000001234567B",
    ///     Delivered = true,
    ///     TimeStamp = "2024-01-15 14:30:00"
    /// };
    /// var success = await client.ConversionClient.SmsConversionAsync(request);
    /// ]]></code>
    /// </example>
    Task<bool> SmsConversionAsync(ConversionRequest request, Credentials creds = null);

    /// <summary>
    ///     Reports a conversion event for a voice call or text-to-speech message.
    ///     Use this to inform Vonage whether the recipient completed your call-to-action
    ///     after receiving a call. This data helps improve call routing.
    /// </summary>
    /// <param name="request">The conversion request containing the call ID and conversion status.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns><c>true</c> if the conversion was successfully reported.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new ConversionRequest
    /// {
    ///     MessageId = "call-id-from-voice-api",
    ///     Delivered = true,
    ///     TimeStamp = "2024-01-15 14:30:00"
    /// };
    /// var success = await client.ConversionClient.VoiceConversionAsync(request);
    /// ]]></code>
    /// </example>
    Task<bool> VoiceConversionAsync(ConversionRequest request, Credentials creds = null);
}