using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using Jose;
using Nexmo.Api.Cryptography;
using Newtonsoft.Json;

namespace Nexmo.Api
{
    public class Jwt
    {
        public static string CreateToken(string appId, string privateKey, ACLs acls = null, uint? expiry = null, string subject = null, Dictionary<string, object> additionalClaims = null)
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
            if (acls != null)
            {
                payload.Add("acl", acls);
            }
            if (expiry != null)
            {
                payload.Add("exp", (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds + expiry);
            }
            if (subject != null)
            {
                payload.Add("sub", subject);
            }
            if (additionalClaims != null)
            {
                foreach (var claim in additionalClaims)
                {
                    if(payload.ContainsKey(claim.Key))
                    {
                        payload[claim.Key] = claim.Value;
                    }
                    else
                    {
                        payload.Add(claim.Key, claim.Value);
                    }
                }
            }

            var rsa = PemParse.DecodePEMKey(privateKey);

            return JWT.Encode(payload, rsa, JwsAlgorithm.RS256);
        }
    }
}