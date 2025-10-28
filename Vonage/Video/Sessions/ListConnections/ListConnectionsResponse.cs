#region
using System.ComponentModel;
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.Video.Sessions.ListConnections;

/// <summary>
/// </summary>
/// <param name="Count">The total number of connections in the session.</param>
/// <param name="ApplicationId">Your Vonage Application ID.</param>
/// <param name="SessionId">The session ID.</param>
/// <param name="Items">The list of connections.</param>
public record ListConnectionsResponse(
    [property: JsonPropertyName("count")] int Count,
    [property: JsonPropertyName("projectId")]
    string ApplicationId,
    [property: JsonPropertyName("sessionId")]
    string SessionId,
    [property: JsonPropertyName("items")] Connection[] Items);

/// <summary>
/// </summary>
/// <param name="ConnectionId">The connection ID.</param>
/// <param name="ConnectionState">The state of the connection.</param>
/// <param name="CreatedAt">
///     The timestamp for when the connection was created, expressed in milliseconds since the Unix
///     epoch (January 1, 1970, 00:00:00 UTC).
/// </param>
public record Connection(
    [property: JsonPropertyName("connectionId")]
    string ConnectionId,
    [property: JsonPropertyName("connectionState")]
    [property: JsonConverter(typeof(EnumDescriptionJsonConverter<Connection.State>))]
    Connection.State ConnectionState,
    [property: JsonPropertyName("createdAt")]
    long CreatedAt)
{
    /// <summary>
    ///     The state of the connection.
    /// </summary>
    public enum State
    {
        /// <summary>
        /// </summary>
        [Description("Connecting")] Connecting,

        /// <summary>
        /// </summary>
        [Description("Connected")] Connected,
    }
}