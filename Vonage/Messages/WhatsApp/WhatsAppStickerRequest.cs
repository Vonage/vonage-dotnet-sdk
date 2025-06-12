#region
using System;
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.Messages.WhatsApp;

/// <summary>
///     Represents a request to send a sticker message on Viber.
/// </summary>
public class WhatsAppStickerRequest<T> : WhatsAppMessageBase
    where T : IStickerContent
{
    /// <summary>
    ///     The sticker content.
    /// </summary>
    [JsonPropertyOrder(9)]
    public T Sticker { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesChannel>))]
    public override MessagesChannel Channel => MessagesChannel.WhatsApp;

    /// <inheritdoc />
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesMessageType>))]
    public override MessagesMessageType MessageType => MessagesMessageType.Sticker;
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