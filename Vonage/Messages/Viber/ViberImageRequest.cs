#region
using System;
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.Messages.Viber;

/// <summary>
///     Represents a request to send an image message on Viber.
/// </summary>
public struct ViberImageRequest : IViberMessage
{
    /// <summary>
    ///     The file information of the request.
    /// </summary>
    [JsonPropertyOrder(6)]
    public CaptionedAttachment Image { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesChannel>))]
    public MessagesChannel Channel => MessagesChannel.ViberService;

    /// <inheritdoc />
    [JsonPropertyOrder(4)]
    public string ClientRef { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(5)]
    [JsonPropertyName("viber_service")]
    public ViberRequestData Data { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(3)]
    public string From { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesMessageType>))]
    public MessagesMessageType MessageType => MessagesMessageType.Image;

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