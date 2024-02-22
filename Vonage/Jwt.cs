using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Jose;
using Vonage.Common.Exceptions;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Request;

namespace Vonage;

/// <inheritdoc />
public class Jwt : ITokenGenerator
{
    /// <inheritdoc />
    public Result<string> GenerateToken(string applicationId, string privateKey,
        Dictionary<string, object> claims = null)
    {
        try
        {
            return CreateToken(applicationId, privateKey, claims);
        }
        catch (Exception exception)
        {
            return Result<string>.FromFailure(new AuthenticationFailure(exception.Message));
        }
    }

    /// <inheritdoc />
    public Result<string> GenerateToken(Credentials credentials, Dictionary<string, object> claims = null) =>
        this.GenerateToken(credentials.ApplicationId, credentials.ApplicationKey, claims);

    /// <summary>
    ///     Creates a token from application id and private key.
    /// </summary>
    /// <param name="appId">The application id.</param>
    /// <param name="privateKey">The private key.</param>
    /// <param name="claims">The additional claims.</param>
    /// <returns>The token.</returns>
    public static string CreateToken(string appId, string privateKey, Dictionary<string, object> claims = null) =>
        CreateTokenWithClaims(appId, privateKey, claims);

    /// <summary>
    ///     Creates a token with custom claims.
    /// </summary>
    /// <param name="appId">The application Id.</param>
    /// <param name="privateKey">The private key.</param>
    /// <param name="claims">The custom claims.</param>
    /// <returns>The token.</returns>
    /// <exception cref="VonageAuthenticationException">When the private key is null or whitespace.</exception>
    private static string CreateTokenWithClaims(string appId, string privateKey, Dictionary<string, object> claims)
    {
        if (string.IsNullOrWhiteSpace(appId) || string.IsNullOrWhiteSpace(privateKey))
        {
            throw VonageAuthenticationException.FromMissingApplicationIdOrPrivateKey();
        }

        var tokenData = new byte[64];
        var rng = RandomNumberGenerator.Create();
        rng.GetBytes(tokenData);
        var jwtTokenId = Convert.ToBase64String(tokenData);
        var payload = new Dictionary<string, object>
        {
            {"iat", (long) (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds},
            {"application_id", appId},
            {"jti", jwtTokenId},
        };
        claims?.ToList().ForEach(claim => payload.Add(claim.Key, claim.Value));
        var rsa = PemParse.DecodePEMKey(privateKey);
        return JWT.Encode(payload, rsa, JwsAlgorithm.RS256);
    }

    /// <summary>
    /// Verifies if a token has been generated using the provided private key.
    /// </summary>
    /// <param name="token">The token to verify.</param>
    /// <param name="privateKey">The private key.</param>
    /// <returns>Whether the token signature is valid.</returns>
    public static bool VerifySignature(string token, string privateKey)
    {
        try
        {
            var rsa = PemParse.DecodePEMKey(privateKey);
            _ = JWT.Decode(token, rsa, JwsAlgorithm.RS256);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}