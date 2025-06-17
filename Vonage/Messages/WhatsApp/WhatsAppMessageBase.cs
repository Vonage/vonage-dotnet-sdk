#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.WhatsApp;

public abstract class WhatsAppMessageBase : MessageRequestBase
{
    /// <summary>
    ///     An optional context used for quoting/replying to a specific message in a conversation. When used, the WhatsApp UI
    ///     will display the new message along with a contextual bubble that displays the quoted/replied to message's content.
    /// </summary>
    [JsonPropertyOrder(8)]
    public WhatsAppContext Context { get; set; }
}