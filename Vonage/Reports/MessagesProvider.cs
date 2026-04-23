#region
using System.ComponentModel;
#endregion

namespace Vonage.Reports;

/// <summary>
///     Represents the messaging provider for a Messages API record.
/// </summary>
public enum MessagesProvider
{
    /// <summary>WhatsApp.</summary>
    [Description("whatsapp")] WhatsApp,

    /// <summary>SMS via Messages API.</summary>
    [Description("sms")] Sms,

    /// <summary>MMS.</summary>
    [Description("mms")] Mms,

    /// <summary>Facebook Messenger.</summary>
    [Description("messenger")] Messenger,

    /// <summary>Viber Service Messages.</summary>
    [Description("viber_service_msg")] ViberServiceMsg,

    /// <summary>Instagram.</summary>
    [Description("instagram")] Instagram,

    /// <summary>RCS Business Messaging.</summary>
    [Description("rcs")] Rcs,
}
