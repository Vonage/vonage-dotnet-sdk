#region
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.Messages.WhatsApp;

/// <summary>
///     Represents a request to send an image message on Viber.
/// </summary>
public class WhatsAppImageRequest : WhatsAppMessageBase
{
    /// <summary>
    ///     The file information of the request.
    /// </summary>
    [JsonPropertyOrder(9)]
    public CaptionedAttachment Image { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesChannel>))]
    public override MessagesChannel Channel => MessagesChannel.WhatsApp;

    /// <inheritdoc />
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesMessageType>))]
    public override MessagesMessageType MessageType => MessagesMessageType.Image;
}