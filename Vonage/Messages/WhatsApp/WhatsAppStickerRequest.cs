#region
using System;
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.Messages.WhatsApp;

/// <summary>
///     Represents a request to send a sticker message on Viber.
/// </summary>
public struct WhatsAppStickerRequest<T> : IWhatsAppMessage
    where T : IStickerContent
{
    /// <summary>
    ///     The sticker content.
    /// </summary>
    [JsonPropertyOrder(5)]
    public T Sticker { get; set; }

    /// <summary>
    ///     An optional context used for quoting/replying to a specific message in a conversation. When used, the WhatsApp UI
    ///     will display the new message along with a contextual bubble that displays the quoted/replied to message's content.
    /// </summary>
    [JsonPropertyOrder(6)]
    public WhatsAppContext Context { get; set; }

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

    /// <inheritdoc />
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesMessageType>))]
    public MessagesMessageType MessageType => MessagesMessageType.Sticker;

    /// <inheritdoc />
    [JsonPropertyOrder(2)]
    public string To { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(7)]
    public string WebhookVersion { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(8)]
    public Uri WebhookUrl { get; set; }
}

/// <summary>
///     Represents a sticker content.
/// </summary>
/// <remarks>
///     This is a 'Marker' interface. It doesn't propose any behavior, and it's sole purpose is to group objects under a
///     common denomination.
/// </remarks>
public interface IStickerContent
{
}

/// <summary>
///     Represents a WhatsApp sticker with a URL content.
/// </summary>
/// <param name="Url">
///     The publicly accessible URL of the sticker image. Supported types are: .webp. See the documentation
///     for more information on sending stickers.
/// </param>
public record UrlSticker(string Url) : IStickerContent;

/// <summary>
///     Represents a WhatsApp sticker with a Guid content.
/// </summary>
/// <param name="Id">
///     The id of the sticker in relation to a specific WhatsApp deployment. See the documentation for more
///     information on sending stickers.
/// </param>
public record IdSticker(Guid Id) : IStickerContent;