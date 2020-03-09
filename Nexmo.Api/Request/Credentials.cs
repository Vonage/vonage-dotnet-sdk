using Nexmo.Api.Cryptography;
using System.IO;
namespace Nexmo.Api.Request
{
    public class Credentials
    {

        /// <summary>
        /// Method to be used for signing SMS Messages
        /// </summary>
        public SmsSignatureGenerator.Method Method { get; set; }
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

        public static Credentials FromApiKeyAndSecret(string apiKey, string apiSecret)
        {
            return new Credentials(){ApiKey = apiKey, ApiSecret = apiSecret};
        }

        /// <summary>
        /// Create Credentials from AppId and Private Key
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static Credentials FromAppIdAndPrivateKey(string appId, string privateKey)
        {
            return new Credentials(){ApplicationId = appId, ApplicationKey = privateKey};
        }

        /// <summary>
        /// Opens private key file and reads it
        /// </summary>
        /// <param name="appId">Your Nexmo Application ID</param>
        /// <param name="privateKeyPath">path to your private Key file</param>
        /// <exception cref="System.UnauthorizedAccessException">Thrown if app doesn't have requried permission to key file</exception>
        /// <exception cref="System.ArgumentException">privateKeyPath is a zero length string, contains only white space or contains one or more invalid charecters</exception>
        /// <exception cref="System.ArgumentNullException">privateKeyPath is null</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, filename, or both exceed the system-defined max length</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file specified in path was not found.</exception>
        /// <exception cref="System.NotSupportedException">path is in an invalid format.</exception>
        /// <returns>CredentialsObject</returns>
        public static Credentials FromAppIdAndPrivateKeyPath(string appId, string privateKeyPath)
        {
            using (var reader = File.OpenText(privateKeyPath))
            {
                var privateKey = reader.ReadToEnd();
                return new Credentials(){ApplicationId = appId, ApplicationKey = privateKey};
            }
        }
        
        /// <summary>
        /// Builds Credentials from 
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="signatureSecret"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Credentials FromApiKeySignatureSecretAndMethod(string apiKey, string signatureSecret,
            SmsSignatureGenerator.Method method)
        {
            return new Credentials(){ApiKey = apiKey, SecuritySecret = signatureSecret, Method = method};
        }
    }
}
