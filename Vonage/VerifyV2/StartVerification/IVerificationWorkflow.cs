using Vonage.Common;

namespace Vonage.VerifyV2.StartVerification;

/// <summary>
///     Represents a verification workflow channel for delivering PIN codes to users. Implementations include
///     <see cref="Sms.SmsWorkflow"/>, <see cref="Voice.VoiceWorkflow"/>, <see cref="Email.EmailWorkflow"/>,
///     <see cref="WhatsApp.WhatsAppWorkflow"/>, <see cref="WhatsAppInteractive.WhatsAppInteractiveWorkflow"/>,
///     and <see cref="SilentAuth.SilentAuthWorkflow"/>.
/// </summary>
public interface IVerificationWorkflow
{
    /// <summary>
    ///     The verification channel identifier (e.g., "sms", "voice", "email", "whatsapp", "whatsapp_interactive", "silent_auth").
    /// </summary>
    string Channel { get; }

    /// <summary>
    ///     Serializes the workflow configuration to JSON format for the API request.
    /// </summary>
    /// <param name="serializer">The JSON serializer to use.</param>
    /// <returns>The JSON representation of the workflow.</returns>
    string Serialize(IJsonSerializer serializer);
}