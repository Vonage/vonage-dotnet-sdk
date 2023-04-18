namespace Vonage.Common.Client.Builders;

/// <summary>
/// Indicates a request has a SessionId.
/// </summary>
public interface IHasSessionId
{
    /// <summary>
    ///     The session Id.
    /// </summary>
    public string SessionId { get; }
}