using Nexmo.Api.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Cryptography
{
    public class SmsSignatureGenerator
    {
        public enum Method
        {
            md5hash,
            md5,
            sha1,
            sha256,
            sha512
        }
        public static string GenerateSignature(string query, string securitySecret, Method method)
        {
            // security secret provided, sort and sign request
            if (method == Method.md5hash)
            {
                query += securitySecret;
                var hashgen = MD5.Create();
                var hash = hashgen.ComputeHash(Encoding.UTF8.GetBytes(query));
                return ByteArrayToHexHelper.ByteArrayToHex(hash).ToLower();
            }
            else
            {
                var securityBytes = Encoding.UTF8.GetBytes(securitySecret);
                var input = Encoding.UTF8.GetBytes(query);
                HMAC hmacGen = new HMACMD5(securityBytes);
                switch (method)
                {
                    case Method.md5:
                        hmacGen = new HMACMD5(securityBytes);
                        break;
                    case Method.sha1:
                        hmacGen = new HMACSHA1(securityBytes);
                        break;
                    case Method.sha256:
                        hmacGen = new HMACSHA256(securityBytes);
                        break;
                    case Method.sha512:
                        hmacGen = new HMACSHA512(securityBytes);
                        break;
                }
                var hmac = hmacGen.ComputeHash(input);
                var sig = ByteArrayToHexHelper.ByteArrayToHex(hmac).ToUpper();
                return sig;
            }
        }
    }
}
