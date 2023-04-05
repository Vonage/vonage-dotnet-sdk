using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Jose;
using Vonage.Common.Exceptions;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Request;
using Vonage.Voice;

namespace Vonage
{
    /// <inheritdoc />
    public class Jwt : ITokenGenerator
    {
        public static string CreateToken(string appId, string privateKey) =>
            CreateTokenWithClaims(appId, privateKey);

        /// <inheritdoc />
        public Result<string> GenerateToken(string applicationId, string privateKey)
        {
            try
            {
                return CreateToken(applicationId, privateKey);
            }
            catch (Exception)
            {
                return Result<string>.FromFailure(new AuthenticationFailure());
            }
        }

        /// <inheritdoc />
        public Result<string> GenerateToken(Credentials credentials) =>
            this.GenerateToken(credentials.ApplicationId, credentials.ApplicationKey);

        protected static string CreateTokenWithClaims(string appId, string privateKey,
            Dictionary<string, object> claims = null)
        {
            if (string.IsNullOrWhiteSpace(privateKey))
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
    }
}