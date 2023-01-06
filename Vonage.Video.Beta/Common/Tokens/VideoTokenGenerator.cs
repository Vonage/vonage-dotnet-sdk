using System.Collections.Generic;
using Vonage.Request;

namespace Vonage.Video.Beta.Common.Tokens;

/// <inheritdoc cref="Vonage.Video.Beta.Common.Tokens.IVideoTokenGenerator" />
public class VideoTokenGenerator : Jwt, IVideoTokenGenerator
{
    /// <inheritdoc />
    public string GenerateToken(Credentials credentials, Dictionary<string, object> claims) =>
        CreateTokenWithClaims(credentials.ApplicationId, credentials.ApplicationKey, claims);
}