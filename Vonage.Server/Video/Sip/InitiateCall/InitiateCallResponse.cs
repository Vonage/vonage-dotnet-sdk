namespace Vonage.Server.Video.Sip.InitiateCall;

/// <summary>
///     Represents the response when initiating a call.
/// </summary>
public struct InitiateCallResponse
{
    /// <summary>
    ///     The OpenTok connection ID for the SIP call's connection in the OpenTok session. You can use this connection ID to
    ///     terminate the SIP call, using the OpenTok REST API.
    /// </summary>

    public string ConnectionId { get; set; }

    /// <summary>
    ///     A unique ID for the SIP call.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    ///     The OpenTok stream ID for the SIP call's stream in the OpenTok session.
    /// </summary>
    public string StreamId { get; set; }
}