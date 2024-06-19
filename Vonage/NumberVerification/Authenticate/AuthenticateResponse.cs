using System.Net.Http.Headers;

namespace Vonage.NumberVerification.Authenticate;

/// <summary>
///     Represents an authentication response.
/// </summary>
/// <param name="AccessToken">The access token.</param>
public record AuthenticateResponse(string AccessToken)
{
    /// <summary>
    /// </summary>
    /// <returns></returns>
    public AuthenticationHeaderValue BuildAuthenticationHeader() =>
        new AuthenticationHeaderValue("Bearer", this.AccessToken);
}