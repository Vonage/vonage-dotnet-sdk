using System.Text.Json.Serialization;
using Vonage.Common.Serialization;
using Vonage.Messages.WhatsApp.ProductMessages;
using Vonage.Messages.WhatsApp.ProductMessages.MultipleItems;

namespace Vonage.Messages.WhatsApp;

/// <summary>
///     Represents a request to send Multiple Items product message on WhatsApp.
/// </summary>
public struct WhatsAppMultipleItemsRequest : IWhatsAppMessage
{
    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesChannel>))]
    public MessagesChannel Channel => MessagesChannel.WhatsApp;

    /// <inheritdoc />
    [JsonPropertyOrder(4)]
    public string ClientRef { get; set; }

    /// <summary>
    ///     The custom content.
    /// </summary>
    [JsonPropertyOrder(5)]
    public ProductMessage<MultipleItemsMessageContent> Custom { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(3)]
    public string From { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesMessageType>))]
    public MessagesMessageType MessageType => MessagesMessageType.Custom;

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