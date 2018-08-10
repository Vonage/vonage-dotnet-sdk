using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Jose;

namespace Nexmo.Api
{
    public class Jwt
    {
        /// <summary>
        /// Generate a JWT with the required/optional private/public claims
        /// </summary>
        /// <param name="appId">The GUID of the Nexmo application this JWT is for</param>
        /// <param name="privateKey">The private key content of the Nexmo application this JWT is for</param>
        /// <param name="sub">The subject of the JWT; usually username</param>
        /// <param name="aclPaths">Specified ACL paths for permissions.
        ///
        /// Stitch example:
        ///
        /// new Dictionary&lt;string, object&gt;
        ///   {
        ///       {"/v1/users/**", new { }},
        ///       {"/v1/conversations/**", new { }},
        ///       {"/v1/sessions/**", new { }},
        ///       {"/v1/devices/**", new { }},
        ///       {"/v1/image/**", new { }},
        ///       {"/v3/media/**", new { }},
        ///       {"/v1/applications/**", new { }},
        ///       {"/v1/push/**", new { }},
        ///       {"/v1/knocking/**", new { }},
        ///   }
        /// </param>
        /// <returns></returns>
        public static string Generate(string appId, string privateKey, string sub = null, Dictionary<string, object> aclPaths = null)
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

            if (!string.IsNullOrEmpty(sub))
            {
                payload["sub"] = sub;
            }

	        if (aclPaths != null && aclPaths.Count > 0)
	        {
		        payload["acl"] = new Dictionary<string, object>
		        {
			        {
				        "paths", aclPaths
			        }
		        };
	        }

            var rsa = PemParse.DecodePEMKey(privateKey);
            
            return JWT.Encode(payload, rsa, JwsAlgorithm.RS256);
        }
    }
}