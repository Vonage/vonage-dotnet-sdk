namespace Vonage.Video.Beta.Common.Tokens;

/// <summary>
///     Represents a token for the Video Client.
/// </summary>
public readonly struct VideoToken
{
    /// <summary>
    ///     The session Id.
    /// </summary>
    public string SessionId { get; }

    /// <summary>
    ///     The token.
    /// </summary>
    public string Token { get; }

    /// <summary>
    ///     Creates a token.
    /// </summary>
    /// <param name="sessionId"> The session Id.</param>
    /// <param name="token"> The token.</param>
    public VideoToken(string sessionId, string token)
    {
        this.SessionId = sessionId;
        this.Token = token;
    }
}