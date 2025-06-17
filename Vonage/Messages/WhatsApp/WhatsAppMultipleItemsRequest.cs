#region
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;
using Vonage.Messages.WhatsApp.ProductMessages;
using Vonage.Messages.WhatsApp.ProductMessages.MultipleItems;
#endregion

namespace Vonage.Messages.WhatsApp;

/// <summary>
///     Represents a request to send Multiple Items product message on WhatsApp.
/// </summary>
public class WhatsAppMultipleItemsRequest : WhatsAppMessageBase
{
    /// <summary>
    ///     The custom content.
    /// </summary>
    [JsonPropertyOrder(9)]
    public ProductMessage<MultipleItemsMessageContent> Custom { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesChannel>))]
    public override MessagesChannel Channel => MessagesChannel.WhatsApp;

    /// <inheritdoc />
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesMessageType>))]
    public override MessagesMessageType MessageType => MessagesMessageType.Custom;
}