#region
using System.ComponentModel;
#endregion

namespace Vonage.VerifyV2;

/// <summary>
///     Represents supported verification channels.
/// </summary>
public enum VerificationChannel
{
    /// <summary>
    ///     SMS
    /// </summary>
    [Description("sms")] Sms,

    /// <summary>
    ///     Voice
    /// </summary>
    [Description("voice")] Voice,

    /// <summary>
    ///     Email
    /// </summary>
    [Description("email")] Email,

    /// <summary>
    ///     Silent Auth
    /// </summary>
    [Description("silent_auth")] SilentAuth,

    /// <summary>
    ///     WhatsApp
    /// </summary>
    [Description("whatsapp")] WhatsApp,

    /// <summary>
    ///     WhatsApp Interactive
    /// </summary>
    [Description("whatsapp_interactive")] WhatsAppInteractive,
}