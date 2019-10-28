using Nexmo.Api.Cryptography;

namespace Nexmo.Api.Request
{
    public class Credentials
    {

        /// <summary>
        /// Method to be used for signing SMS Messages
        /// </summary>
        public SmsSignatureGenerator.Method Method { get; set; }

        /// <summary>
        /// Access control list. The Nexmo Client SDK uses this as a permission system for users. Read more about it in the ACL overview
        /// </summary>
        public ACLs Acls { get; set; }

        /// <summary>
        /// The "subject". The subject, in this case, will be the name of the user created and associated with your Nexmo Application 
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Seconds before a JWT generated with this will expire
        /// </summary>
        public uint ExpirationSeconds { get; set; }
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
    }
}
