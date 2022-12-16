namespace Vonage.Video.Beta.Video.Sessions;

/// <summary>
///     Defines values for a session's Archive mode.
/// </summary>
public enum ArchiveMode
{
    /// <summary>
    ///     The session is not archived automatically. To archive the session, you can call the
    ///     <see cref="ToBeDefined" /> method.
    /// </summary>
    Manual,

    /// <summary>
    ///     The session is archived automatically (as soon as there are clients publishing streams
    ///     to the session).
    /// </summary>
    Always,
}