namespace Vonage.SimSwap.Authenticate;

/// <summary>
///     Represents an authentication response.
/// </summary>
/// <param name="AccessToken">The access token.</param>
public record AuthenticateResponse(string AccessToken);