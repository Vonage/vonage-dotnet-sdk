using System;
using Vonage.Request;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Common.Tokens;

/// <inheritdoc cref="Vonage.Video.Beta.Common.Tokens.IVideoTokenGenerator" />
public class VideoTokenGenerator : Jwt, IVideoTokenGenerator
{
    /// <inheritdoc />
    public Result<VideoToken> GenerateToken(Credentials credentials, TokenAdditionalClaims claims)
    {
        try
        {
            var tokenValue = CreateTokenWithClaims(credentials.ApplicationId, credentials.ApplicationKey,
                claims.ToDataDictionary());
            return Result<VideoToken>.FromSuccess(new VideoToken(claims.SessionId, tokenValue));
        }
        catch (Exception exception)
        {
            return Result<VideoToken>.FromFailure(ResultFailure.FromErrorMessage(exception.Message));
        }
    }
}