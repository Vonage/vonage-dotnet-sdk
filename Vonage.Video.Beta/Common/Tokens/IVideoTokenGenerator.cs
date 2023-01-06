using System.Collections.Generic;
using Vonage.Request;

namespace Vonage.Video.Beta.Common.Tokens;

/// <summary>
///     Represents a specific token generator for the Video Client.
/// </summary>
public interface IVideoTokenGenerator
{
    /// <summary>
    ///     Generates a token.
    /// </summary>
    /// <param name="credentials">The application credentials.</param>
    /// <param name="claims">Additional claims for the token.</param>
    /// <returns>The token.</returns>
    string GenerateToken(Credentials credentials, Dictionary<string, object> claims);
}