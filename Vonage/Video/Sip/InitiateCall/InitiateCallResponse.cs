using System;

namespace Vonage.Video.Sip.InitiateCall;

/// <summary>
///     Represents the response when initiating a call.
/// </summary>
public struct InitiateCallResponse
{
    /// <summary>
    ///     The Vonage Video connection ID for the SIP call's connection in the Vonage Video session. You can use this
    ///     connection ID to terminate the SIP call, using the Vonage Video REST API.
    /// </summary>

    public Guid ConnectionId { get; set; }

    /// <summary>
    ///     A unique ID for the SIP call.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    ///     The Vonage Video stream ID for the SIP call's stream in the Vonage Video session.
    /// </summary>
    public string StreamId { get; set; }
}