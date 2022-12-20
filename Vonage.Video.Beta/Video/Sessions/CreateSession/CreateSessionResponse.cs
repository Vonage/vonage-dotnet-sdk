namespace Vonage.Video.Beta.Video.Sessions.CreateSession;

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
    ///     Creates a new response.
    /// </summary>
    /// <param name="sessionId">The created session Id.</param>
    public CreateSessionResponse(string sessionId) => this.SessionId = sessionId;

    /// <summary>
    ///     Gets or sets the session Id.
    /// </summary>
    /// <remarks>
    ///     This struct should be read-only. The setter is mandatory for deserialization.
    /// </remarks>
    public string SessionId { get; set; }
}