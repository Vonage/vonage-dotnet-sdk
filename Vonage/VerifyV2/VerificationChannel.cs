#region
using System.ComponentModel;
#endregion

namespace Vonage.VerifyV2;

/// <summary>
///     Defines the communication channels available for sending verification PIN codes to users.
/// </summary>
public enum VerificationChannel
{
    /// <summary>
    ///     Send the verification code via SMS text message. Supports optional app_hash for Android auto-detection and entity_id/content_id for Indian carriers.
    /// </summary>
    [Description("sms")] Sms,

    /// <summary>
    ///     Deliver the verification code via text-to-speech voice call to the user's phone.
    /// </summary>
    [Description("voice")] Voice,

    /// <summary>
    ///     Send the verification code via email to the user's email address.
    /// </summary>
    [Description("email")] Email,

    /// <summary>
    ///     Verify the user's phone number silently using mobile network authentication without requiring a PIN code. The device must be connected via cellular data.
    /// </summary>
    [Description("silent_auth")] SilentAuth,

    /// <summary>
    ///     Send the verification code via WhatsApp message. Requires a WhatsApp Business Account (WABA) connected sender number.
    /// </summary>
    [Description("whatsapp")] WhatsApp,

    /// <summary>
    ///     Send the verification code via WhatsApp interactive message with a one-tap button for automatic code submission.
    /// </summary>
    [Description("whatsapp_interactive")] WhatsAppInteractive,
}