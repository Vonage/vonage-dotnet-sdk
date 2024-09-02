namespace Vonage.Messages;

/// <summary>
///     Represents a request to update a message.
/// </summary>
public interface IUpdateMessageRequest
{
    /// <summary>
    ///     UUID of the message to be updated
    /// </summary>
    string MessageUuid { get; }

    /// <summary>
    ///     The status to set for the message.
    /// </summary>
    string Status { get; }
}