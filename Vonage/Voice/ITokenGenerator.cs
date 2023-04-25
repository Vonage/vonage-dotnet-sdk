using System.Collections.Generic;
using Vonage.Common.Monads;
using Vonage.Request;

namespace Vonage.Voice;

/// <summary>
///     Represents a token generator.
/// </summary>
public interface ITokenGenerator
{
    /// <summary>
    ///     Generates a token.
    /// </summary>
    /// <param name="applicationId">The application Id.</param>
    /// <param name="privateKey">The application private key.</param>
    /// <param name="claims">The additional claims.</param>
    /// <returns>The token.</returns>
    Result<string> GenerateToken(string applicationId, string privateKey, Dictionary<string, object> claims = null);

    /// <summary>
    ///     Generates a token.
    /// </summary>
    /// <param name="credentials">The application credentials.</param>
    /// <param name="claims">The additional claims.</param>
    /// <returns>The token.</returns>
    Result<string> GenerateToken(Credentials credentials, Dictionary<string, object> claims = null);
}