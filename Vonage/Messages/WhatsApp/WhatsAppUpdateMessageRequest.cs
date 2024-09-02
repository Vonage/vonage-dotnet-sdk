#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.WhatsApp;

/// <summary>
///     Update message request for WhatsApp.
/// </summary>
public readonly struct WhatsAppUpdateMessageRequest : IUpdateMessageRequest
{
    /// <inheritdoc />
    [JsonIgnore]
    public string MessageUuid { get; private init; }

    /// <inheritdoc />
    public string Status { get; private init; }

    /// <summary>
    ///     Build an update message request for WhatsApp.
    /// </summary>
    /// <param name="messageUuid">UUID of the message to be updated</param>
    /// <returns>The request.</returns>
    public static WhatsAppUpdateMessageRequest Build(string messageUuid) => new WhatsAppUpdateMessageRequest
    {
        MessageUuid = messageUuid,
        Status = "read",
    };
}