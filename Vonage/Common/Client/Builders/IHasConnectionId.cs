namespace Vonage.Common.Client.Builders;

/// <summary>
///     Indicates a request has a connection Id.
/// </summary>
public interface IHasConnectionId
{
    /// <summary>
    ///     The connection Id.
    /// </summary>
    string ConnectionId { get; }
}