using System.Security.Cryptography;
using System.Text;

namespace Nexmo.Api.Request
{
    public class Credentials
    {

        public enum SigningMethod
        {
            md5hash,
            md5,
            sha1,
            sha256,
            sha512
        }

        public SigningMethod Method { get; set; }
        /// <summary>
        /// Nexmo API Key (from your account dashboard)
        /// </summary>
        public string ApiKey { get; set; }
        /// <summary>
        /// Nexmo API Secret (from your account dashboard)
        /// </summary>
        public string ApiSecret { get; set; }
        /// <summary>
        /// Signature Secret (from API settings section of your account settings)
        /// </summary>
        public string SecuritySecret { get; set; }
        /// <summary>
        /// Application ID (GUID)
        /// </summary>
        public string ApplicationId { get; set; }
        /// <summary>
        /// Application private key contents
        /// This is the actual key file contents and NOT a path to the key file!
        /// </summary>
        public string ApplicationKey { get; set; }

        /// <summary>
        /// (Optional) App useragent value to pass with every request
        /// </summary>
        public string AppUserAgent { get; set; }

        public static string GenerateSignature(string query, string securitySecret, Credentials.SigningMethod method)
        {
            var queryToSign = "&" + query;
            queryToSign = queryToSign.Remove(queryToSign.Length - 1);
            // security secret provided, sort and sign request
            if (method == Credentials.SigningMethod.md5hash)
            {
                queryToSign += securitySecret;
                var hashgen = MD5.Create();
                var hash = hashgen.ComputeHash(Encoding.UTF8.GetBytes(queryToSign));
                return ByteArrayToHexHelper.ByteArrayToHex(hash).ToLower();
            }
            else
            {
                var securityBytes = Encoding.UTF8.GetBytes(securitySecret);
                var input = Encoding.UTF8.GetBytes(queryToSign);
                HMAC hmacGen = new HMACMD5(securityBytes);
                switch (method)
                {
                    case Credentials.SigningMethod.md5:
                        hmacGen = new HMACMD5(securityBytes);
                        break;
                    case Credentials.SigningMethod.sha1:
                        hmacGen = new HMACSHA1(securityBytes);
                        break;
                    case Credentials.SigningMethod.sha256:
                        hmacGen = new HMACSHA256(securityBytes);
                        break;
                    case Credentials.SigningMethod.sha512:
                        hmacGen = new HMACSHA512(securityBytes);
                        break;
                }
                var hmac = hmacGen.ComputeHash(input);
                var sig = ByteArrayToHexHelper.ByteArrayToHex(hmac).ToUpper();
                return sig;
            }
        }

        public Credentials()
        {

        }

        public Credentials (string nexmoApiKey, string nexmoApiSecret)
        {
            ApiKey = nexmoApiKey;
            ApiSecret = nexmoApiSecret;
        }

        public Credentials(string nexmoApiKey, string nexmoApiSecret, string nexmoApplicationId, string nexmoApplicationPrivateKey)
        {
            ApiKey = nexmoApiKey;
            ApiSecret = nexmoApiSecret;
            ApplicationId = nexmoApplicationId;
            ApplicationKey = nexmoApplicationPrivateKey;
        }
    }
}
