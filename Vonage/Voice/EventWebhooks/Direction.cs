namespace Vonage.Voice.EventWebhooks;

/// <summary>
///     Defines the direction of a voice call.
/// </summary>
public enum Direction
{
    /// <summary>
    ///     An incoming call received by your application.
    /// </summary>
    inbound = 1,

    /// <summary>
    ///     An outgoing call initiated by your application.
    /// </summary>
    outbound = 2,
}