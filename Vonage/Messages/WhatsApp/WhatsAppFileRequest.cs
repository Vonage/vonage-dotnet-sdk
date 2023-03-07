using System.Text.Json.Serialization;
using Vonage.Common.Serialization;

namespace Vonage.Messages.WhatsApp;

/// <summary>
///     Represents a request to send a file message on Viber.
/// </summary>
public class WhatsAppFileRequest : IWhatsAppMessage
{
    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesChannel>))]
    public MessagesChannel Channel => MessagesChannel.WhatsApp;

    /// <inheritdoc />
    [JsonPropertyOrder(4)]
    public string ClientRef { get; set; }

    /// <summary>
    ///     The file information of the request.
    /// </summary>
    [JsonPropertyOrder(5)]
    public CaptionedAttachment File { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(3)]
    public string From { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesMessageType>))]
    public MessagesMessageType MessageType => MessagesMessageType.File;

    /// <inheritdoc />
    [JsonPropertyOrder(2)]
    public string To { get; set; }
}