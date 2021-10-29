using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Jose;

namespace Vonage
{    
    public class Jwt
    {
        public static string CreateToken(string appId, string privateKey)
        {
            var tokenData = new byte[64];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(tokenData);
            var jwtTokenId = Convert.ToBase64String(tokenData);

            var payload = new Dictionary<string, object>
            {
                { "iat", (long) (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds },
                { "application_id", appId },
                { "jti", jwtTokenId }
            };

            var rsa = PemParse.DecodePEMKey(privateKey);
            
            return JWT.Encode(payload, rsa, JwsAlgorithm.RS256);
        }
    }
}