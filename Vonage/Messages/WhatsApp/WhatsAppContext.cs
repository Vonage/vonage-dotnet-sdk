using System.Text.Json.Serialization;

namespace Vonage.Messages.WhatsApp;

/// <summary>
///     Represents an optional context used for quoting/replying to a specific message in a conversation. When used, the
///     WhatsApp UI will display the new message along with a contextual bubble that displays the quoted/replied to
///     message's content.
/// </summary>
/// <param name="MessageId">The UUID of the message being replied to/quoted.</param>
public record WhatsAppContext(
    [property: JsonPropertyName("message_uuid")]
    string MessageId);