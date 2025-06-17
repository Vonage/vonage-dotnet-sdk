#region
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.Messages.WhatsApp;

/// <summary>
///     Represents a request to send a custom message on WhatsApp.
/// </summary>
public class WhatsAppCustomRequest : WhatsAppMessageBase
{
    /// <summary>
    ///     A custom payload, which is passed directly to WhatsApp for certain features such as
    ///     templates and interactive messages.
    /// </summary>
    [JsonPropertyOrder(9)]
    public object Custom { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesChannel>))]
    public override MessagesChannel Channel => MessagesChannel.WhatsApp;

    /// <inheritdoc />
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesMessageType>))]
    public override MessagesMessageType MessageType => MessagesMessageType.Custom;
}