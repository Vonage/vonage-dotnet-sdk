using System.Text.Json.Serialization;

namespace Vonage.Video.Sessions.CreateSession;

/// <summary>
///     Represents the response when a session has been created.
/// </summary>
public struct CreateSessionResponse
{
    /// <summary>
    ///     Indicates no session was created.
    /// </summary>
    public const string NoSessionCreated = "No session was created.";

    /// <summary>
    ///     Gets or sets the session Id.
    /// </summary>
    [JsonPropertyName("session_id")]
    public string SessionId { get; }

    /// <summary>
    ///     Creates a new response.
    /// </summary>
    /// <param name="sessionId">The created session Id.</param>
    [JsonConstructor]
    public CreateSessionResponse(string sessionId) => this.SessionId = sessionId;
}