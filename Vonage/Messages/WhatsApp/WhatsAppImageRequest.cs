using System.Text.Json.Serialization;
using Vonage.Common.Serialization;

namespace Vonage.Messages.WhatsApp;

/// <summary>
///     Represents a request to send an image message on Viber.
/// </summary>
public struct WhatsAppImageRequest : IWhatsAppMessage
{
    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesChannel>))]
    public MessagesChannel Channel => MessagesChannel.WhatsApp;

    /// <inheritdoc />
    [JsonPropertyOrder(4)]
    public string ClientRef { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(3)]
    public string From { get; set; }

    /// <summary>
    ///    The file information of the request.
    /// </summary>
    [JsonPropertyOrder(5)]
    public CaptionedAttachment Image { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesMessageType>))]
    public MessagesMessageType MessageType => MessagesMessageType.Image;

    /// <inheritdoc />
    [JsonPropertyOrder(2)]
    public string To { get; set; }

    /// <summary>
    ///     An optional context used for quoting/replying to a specific message in a conversation. When used, the WhatsApp UI
    ///     will display the new message along with a contextual bubble that displays the quoted/replied to message's content.
    /// </summary>
    [JsonPropertyOrder(6)]
    public WhatsAppContext Context { get; set; }
}