using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Jose;

namespace Nexmo.Api
{
    internal class Jwt
    {
        internal static string CreateToken(string appId, string privateKeyFile)
        {
            string jwtTokenId;
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                var tokenData = new byte[64];
                rng.GetBytes(tokenData);

                jwtTokenId = Convert.ToBase64String(tokenData);
            }

            var payload = new Dictionary<string, object>
            {
                { "iat", (long) (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds },
                { "application_id", appId },
                { "jti", jwtTokenId }
            };

            var pemContents = File.ReadAllText(privateKeyFile);
            var rsa = PemParse.DecodePEMKey(pemContents);

            return JWT.Encode(payload, rsa, JwsAlgorithm.RS256);
        }
    }
}