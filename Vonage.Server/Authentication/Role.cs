using System.ComponentModel;

namespace Vonage.Server.Authentication;

/// <summary>
///     Defines values for the role parameter of the GenerateToken method of the OpenTok class.
/// </summary>
public enum Role
{
    /// <summary>
    ///     A publisher can publish streams, subscribe to streams, and signal. (This is the default
    ///     value if you do not set a role when calling GenerateToken method of the OpenTok class.
    /// </summary>
    [Description("publisher")] Publisher,

    /// <summary>
    ///     A subscriber can only subscribe to streams.
    /// </summary>
    [Description("subscriber")] Subscriber,

    /// <summary>
    ///     In addition to the privileges granted to a publisher, a moderator can perform moderation
    ///     functions, such as forcing clients to disconnect, to stop publishing streams, or to
    ///     mute audio in published streams. See the
    ///     <a href="https://tokbox.com/developer/guides/moderation/">Moderation developer guide</a>.
    /// </summary>
    [Description("moderator")] Moderator,
}