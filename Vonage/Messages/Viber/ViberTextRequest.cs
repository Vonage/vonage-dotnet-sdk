#region
using System;
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.Messages.Viber;

/// <summary>
///     Represents a request to send a text message on Viber.
/// </summary>
public struct ViberTextRequest : IViberMessage
{
    /// <summary>
    ///     The text of message to send; limited to 640 characters, including unicode.
    /// </summary>
    [JsonPropertyOrder(2)]
    public string Text { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesChannel>))]
    public MessagesChannel Channel => MessagesChannel.ViberService;

    /// <inheritdoc />
    [JsonPropertyOrder(5)]
    public string ClientRef { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(6)]
    [JsonPropertyName("viber_service")]
    public ViberRequestData Data { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(4)]
    public string From { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesMessageType>))]
    public MessagesMessageType MessageType => MessagesMessageType.Text;

    /// <inheritdoc />
    [JsonPropertyOrder(3)]
    public string To { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(7)]
    public string WebhookVersion { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(8)]
    public Uri WebhookUrl { get; set; }
}