namespace Vonage.Server.Video.Sessions;

/// <summary>
///     Defines values for a session's Media mode.
/// </summary>
public enum MediaMode
{
    /// <summary>
    ///     The session will transmit streams using the OpenTok Media Router.
    /// </summary>
    Routed,

    /// <summary>
    ///     The session will attempt to transmit streams directly between clients. If two clients
    ///     cannot send and receive each others' streams, due to firewalls on the clients' networks,
    ///     their streams will be relayed using the OpenTok TURN Server.
    /// </summary>
    Relayed,
}