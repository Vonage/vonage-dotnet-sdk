using Vonage.Common.Monads;
using Vonage.Request;

namespace Vonage.Video.Authentication;

/// <inheritdoc cref="IVideoTokenGenerator" />
public class VideoTokenGenerator : Jwt, IVideoTokenGenerator
{
    /// <inheritdoc />
    public Result<VideoToken> GenerateToken(Credentials credentials, Result<TokenAdditionalClaims> claims) =>
        claims.Bind(validClaim => this.BuildVideoToken(credentials, validClaim));

    private Result<VideoToken> BuildVideoToken(Credentials credentials, TokenAdditionalClaims validClaim) =>
        this.GenerateToken(credentials, validClaim.ToDataDictionary())
            .Map(token => new VideoToken(validClaim.SessionId, token));
}