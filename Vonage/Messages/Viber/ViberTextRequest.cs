#region
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.Messages.Viber;

/// <summary>
///     Represents a request to send a text message on Viber.
/// </summary>
public class ViberTextRequest : ViberMessageBase
{
    /// <summary>
    ///     The text of message to send; limited to 640 characters, including unicode.
    /// </summary>
    [JsonPropertyOrder(9)]
    public string Text { get; set; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesChannel>))]
    public override MessagesChannel Channel => MessagesChannel.ViberService;

    /// <inheritdoc />
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<MessagesMessageType>))]
    public override MessagesMessageType MessageType => MessagesMessageType.Text;
}