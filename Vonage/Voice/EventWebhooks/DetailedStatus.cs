namespace Vonage.Voice.EventWebhooks;

/// <summary>
///     Provides detailed status information about why a call ended or failed. Included in call status webhook events.
/// </summary>
public enum DetailedStatus
{
    /// <summary>
    ///     No additional detail was provided for this call status.
    /// </summary>
    no_detail,

    /// <summary>
    ///     A detail was provided by the carrier but does not match any known enum value.
    /// </summary>
    unmapped_detail,

    /// <summary>
    ///     The destination number is not a valid phone number.
    /// </summary>
    invalid_number,

    /// <summary>
    ///     The call was rejected by the carrier due to network restrictions.
    /// </summary>
    restricted,

    /// <summary>
    ///     The call was actively rejected by the callee.
    /// </summary>
    declined,

    /// <summary>
    ///     The call could not be routed to the destination number.
    /// </summary>
    cannot_route,

    /// <summary>
    ///     The destination number is no longer in service.
    /// </summary>
    number_out_of_service,

    /// <summary>
    ///     An internal server error or system failure occurred during call processing.
    /// </summary>
    internal_error,

    /// <summary>
    ///     The carrier timed out while attempting to connect the call.
    /// </summary>
    carrier_timeout,

    /// <summary>
    ///     The callee is temporarily unavailable (e.g., phone off, out of coverage area).
    /// </summary>
    unavailable
}