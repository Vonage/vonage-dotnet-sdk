using System.Text.Json.Serialization;
using Vonage.Common.Serialization;

namespace Vonage.Messages;

public abstract class MessageRequestBase : IMessage
{
    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesChannel>))]
    public abstract MessagesChannel Channel { get; }

    /// <inheritdoc />
    [JsonPropertyOrder(5)]
    public string ClientRef { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(4)]
    public string From { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesMessageType>))]
    public abstract MessagesMessageType MessageType { get; }

    /// <inheritdoc />
    [JsonPropertyOrder(3)]
    public string To { get; set; }
}