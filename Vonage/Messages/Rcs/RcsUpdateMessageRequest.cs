#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Rcs;

/// <summary>
///     Update message request for RCS.
/// </summary>
public struct RcsUpdateMessageRequest : IUpdateMessageRequest
{
    /// <inheritdoc />
    [JsonIgnore]
    public string MessageUuid { get; private init; }

    /// <inheritdoc />
    public string Status { get; private init; }

    /// <summary>
    ///     Build an update message request for RCS.
    /// </summary>
    /// <param name="messageUuid">UUID of the message to be updated</param>
    /// <returns>The request.</returns>
    public static RcsUpdateMessageRequest Build(string messageUuid) => new RcsUpdateMessageRequest
    {
        MessageUuid = messageUuid,
        Status = "revoked",
    };
}