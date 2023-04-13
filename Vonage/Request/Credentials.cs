using System.IO;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Cryptography;

namespace Vonage.Request
{
    /// <summary>
    ///     Represents credentials for Vonage APIs.
    /// </summary>
    public class Credentials
    {
        private Credentials()
        {
        }

        /// <summary>
        ///     Vonage API Key (from your account dashboard)
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        ///     Vonage API Secret (from your account dashboard)
        /// </summary>
        public string ApiSecret { get; set; }

        /// <summary>
        ///     Application ID (GUID)
        /// </summary>
        public string ApplicationId { get; set; }

        /// <summary>
        ///     Application private key contents
        ///     This is the actual key file contents and NOT a path to the key file!
        /// </summary>
        public string ApplicationKey { get; set; }

        /// <summary>
        ///     (Optional) App useragent value to pass with every request
        /// </summary>
        public string AppUserAgent { get; set; }

        /// <summary>
        ///     Method to be used for signing SMS Messages
        /// </summary>
        public SmsSignatureGenerator.Method Method { get; set; }

        /// <summary>
        ///     Signature Secret (from API settings section of your account settings)
        /// </summary>
        public string SecuritySecret { get; set; }

        /// <summary>
        ///     Builds credentials from ApiKey and ApiSecret.
        /// </summary>
        /// <param name="apiKey">The api key.</param>
        /// <param name="apiSecret">The api secret.</param>
        /// <returns>The credentials.</returns>
        public static Credentials FromApiKeyAndSecret(string apiKey, string apiSecret) =>
            new() {ApiKey = apiKey, ApiSecret = apiSecret};

        /// <summary>
        ///     Builds Credentials from ApiKey, SignatureSecret and Method.
        /// </summary>
        /// <param name="apiKey">The api key.</param>
        /// <param name="signatureSecret">The signature secret.</param>
        /// <param name="signatureMethod">The signature method.</param>
        /// <returns>The credentials.</returns>
        public static Credentials FromApiKeySignatureSecretAndMethod(string apiKey, string signatureSecret,
            SmsSignatureGenerator.Method signatureMethod) =>
            new() {ApiKey = apiKey, SecuritySecret = signatureSecret, Method = signatureMethod};

        /// <summary>
        ///     Builds Credentials from ApplicationId and PrivateKey.
        /// </summary>
        /// <param name="appId">The application id.</param>
        /// <param name="privateKey">The private key.</param>
        /// <returns>The credentials.</returns>
        public static Credentials FromAppIdAndPrivateKey(string appId, string privateKey) =>
            new() {ApplicationId = appId, ApplicationKey = privateKey};

        /// <summary>
        ///     Builds Credentials from ApplicationId and PrivateKey file.
        /// </summary>
        /// <param name="appId">Your Vonage Application ID</param>
        /// <param name="privateKeyPath">path to your private Key file</param>
        /// <exception cref="System.UnauthorizedAccessException">Thrown if app doesn't have required permission to key file</exception>
        /// <exception cref="System.ArgumentException">
        ///     privateKeyPath is a zero length string, contains only white space or
        ///     contains one or more invalid characters
        /// </exception>
        /// <exception cref="System.ArgumentNullException">privateKeyPath is null</exception>
        /// <exception cref="System.IO.PathTooLongException">
        ///     The specified path, filename, or both exceed the system-defined max
        ///     length
        /// </exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">
        ///     The specified path is invalid, (for example, it is on an
        ///     unmapped drive).
        /// </exception>
        /// <exception cref="System.IO.FileNotFoundException">The file specified in path was not found.</exception>
        /// <exception cref="System.NotSupportedException">path is in an invalid format.</exception>
        /// <returns>CredentialsObject</returns>
        public static Credentials FromAppIdAndPrivateKeyPath(string appId, string privateKeyPath)
        {
            using var reader = File.OpenText(privateKeyPath);
            var privateKey = reader.ReadToEnd();
            return new Credentials {ApplicationId = appId, ApplicationKey = privateKey};
        }

        /// <summary>
        ///     Provides the preferred authentication type.
        /// </summary>
        /// <returns>The authentication type if it matches any criteria. A AuthenticationFailure otherwise.</returns>
        /// <remarks>
        ///     Bearer will always be preferred over Basic if possible.
        /// </remarks>
        public Result<AuthType> GetPreferredAuthenticationType()
        {
            if (!string.IsNullOrWhiteSpace(this.ApplicationId) && !string.IsNullOrWhiteSpace(this.ApplicationKey))
            {
                return AuthType.Bearer;
            }

            if (!string.IsNullOrWhiteSpace(this.ApiKey) && !string.IsNullOrWhiteSpace(this.ApiSecret))
            {
                return AuthType.Basic;
            }

            return Result<AuthType>.FromFailure(new AuthenticationFailure());
        }

        /// <summary>
        ///     Returns the user agent from credentials if not null, from configuration otherwise.
        /// </summary>
        /// <returns>The user agent.</returns>
        public string GetUserAgent() =>
            this.AppUserAgent ?? Configuration.Instance.Settings["appSettings:Vonage.UserAgent"];
    }
}