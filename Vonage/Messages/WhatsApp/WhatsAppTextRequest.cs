#region
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.Messages.WhatsApp;

/// <summary>
///     Represents a request to send a text message on WhatsApp.
/// </summary>
public class WhatsAppTextRequest : WhatsAppMessageBase
{
    /// <summary>
    ///     The text of message to send; limited to 4096 characters, including unicode.
    /// </summary>
    [JsonPropertyOrder(9)]
    public string Text { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesChannel>))]
    public override MessagesChannel Channel => MessagesChannel.WhatsApp;

    /// <inheritdoc />
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesMessageType>))]
    public override MessagesMessageType MessageType => MessagesMessageType.Text;
}