#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.WhatsApp;

/// <summary>
///     Update message request for WhatsApp.
/// </summary>
public readonly struct WhatsAppUpdateMessageRequest : IUpdateMessageRequest
{
    /// <summary>
    ///     The replying indicator object.
    /// </summary>
    public WhatsAppReplyingIndicator ReplyingIndicator { get; private init; }

    /// <inheritdoc />
    [JsonIgnore]
    public string MessageUuid { get; private init; }

    /// <inheritdoc />
    public string Status => "read";

    /// <summary>
    ///     Build an update message request for WhatsApp.
    /// </summary>
    /// <param name="messageUuid">UUID of the message to be updated</param>
    /// <returns>The request.</returns>
    public static WhatsAppUpdateMessageRequest Build(string messageUuid) =>
        new WhatsAppUpdateMessageRequest {MessageUuid = messageUuid};

    /// <summary>
    ///     Build an update message request for WhatsApp.
    /// </summary>
    /// <param name="messageUuid">UUID of the message to be updated</param>
    /// <param name="replyingIndicator"> The replying indicator object.</param>
    /// <returns>The request.</returns>
    public static WhatsAppUpdateMessageRequest Build(string messageUuid, WhatsAppReplyingIndicator replyingIndicator) =>
        new WhatsAppUpdateMessageRequest
        {
            MessageUuid = messageUuid,
            ReplyingIndicator = replyingIndicator,
        };
}

/// <summary>
///     The replying indicator object.
/// </summary>
/// <param name="Show">Whether to show the replying indicator to the WhatsApp user.</param>
public record WhatsAppReplyingIndicator(bool Show)
{
    /// <summary>
    ///     The type of indicator to the WhatsApp user. The replying indicator will be dismissed once you respond, or after 25
    ///     seconds, whichever comes first. To prevent a poor user experience, only display a replying indicator if you are
    ///     going to respond. Must be 'text'.
    /// </summary>
    public string Type => "text";
}