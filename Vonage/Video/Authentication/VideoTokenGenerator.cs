using System;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Request;

namespace Vonage.Video.Authentication;

/// <inheritdoc cref="IVideoTokenGenerator" />
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