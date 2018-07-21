using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Jose;

namespace Nexmo.Api
{
    internal class Jwt
    {
        internal static string CreateToken(string appId, string privateKey, string sub = null)
        {
            var tokenData = new byte[64];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(tokenData);
            var jwtTokenId = Convert.ToBase64String(tokenData);

            var payload = new Dictionary<string, object>
            {
                { "iat", (long) (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds },
                { "application_id", appId },
                { "jti", jwtTokenId },
                // TODO: Hardcoded
                { "acl", new Dictionary<string, object>
                    {
                        { "paths", new Dictionary<string, object>
                            {
                                { "/v1/users/**", new {} },
                                { "/v1/conversations/**", new {} },
                                { "/v1/sessions/**", new {} },
                                { "/v1/devices/**", new {} },
                                { "/v1/image/**", new {} },
                                { "/v3/media/**", new {} },
                                { "/v1/applications/**", new {} },
                                { "/v1/push/**", new {} },
                                { "/v1/knocking/**", new {} },
                            }
                        }
                    }
                }
            };

            if (!string.IsNullOrEmpty(sub))
            {
                payload["sub"] = sub;
            }

            var rsa = PemParse.DecodePEMKey(privateKey);
            
            return JWT.Encode(payload, rsa, JwsAlgorithm.RS256);
        }
    }
}