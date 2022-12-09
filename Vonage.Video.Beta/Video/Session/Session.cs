using Vonage.Video.Beta.Video.Session.CreateSession;

namespace Vonage.Video.Beta.Video.Session;

public record Session
{
}

/// <summary>
///     Defines values for the mediaMode parameter of the <see cref="CreateSessionRequest.Parse" /> method of the
///     <see cref="CreateSessionRequest" /> class.
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

/// <summary>
///     Defines values for the archiveMode property of the <see cref="Session" /> object.
///     You also use these values for the archiveMode parameter of the <see cref="OpenTok.CreateSession" /> method.
/// </summary>
public enum ArchiveMode
{
    /// <summary>
    ///     The session is not archived automatically. To archive the session, you can call the
    ///     <see cref="OpenTok.StartArchive" /> method.
    /// </summary>
    Manual,

    /// <summary>
    ///     The session is archived automatically (as soon as there are clients publishing streams
    ///     to the session).
    /// </summary>
    Always,
}