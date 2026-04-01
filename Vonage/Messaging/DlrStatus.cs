namespace Vonage.Messaging;

/// <summary>
///     Represents the delivery status of an SMS message as reported in a delivery receipt (DLR).
/// </summary>
public enum DlrStatus
{
    /// <summary>
    ///     The message was successfully delivered to the recipient's handset.
    /// </summary>
    delivered = 0,

    /// <summary>
    ///     The message delivery timed out before reaching the recipient.
    /// </summary>
    expired = 1,

    /// <summary>
    ///     The message delivery failed. Check the error code for details.
    /// </summary>
    failed = 2,

    /// <summary>
    ///     The message was rejected by the carrier or recipient.
    /// </summary>
    rejected = 3,

    /// <summary>
    ///     The message was accepted by the carrier for delivery.
    /// </summary>
    accepted = 4,

    /// <summary>
    ///     The message is being buffered by the carrier for later delivery.
    /// </summary>
    buffered = 5,

    /// <summary>
    ///     The delivery status could not be determined.
    /// </summary>
    unknown = 6,
}