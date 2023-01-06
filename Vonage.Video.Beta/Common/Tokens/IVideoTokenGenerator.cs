﻿using Vonage.Request;
using Vonage.Video.Beta.Common.Monads;

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
    /// <returns>A success state with the token if the parsing succeeded. A failure state with an error if it failed.</returns>
    Result<VideoToken> GenerateToken(Credentials credentials, TokenAdditionalClaims claims);
}