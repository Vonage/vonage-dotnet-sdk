using System;

namespace Vonage.Common.Exceptions
{
    /// <summary>
    ///     Represents an issue related to authentication.
    /// </summary>
    public class VonageAuthenticationException : Exception
    {
        private VonageAuthenticationException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Creates an exception indicating the ApiKey or ApiSecret are missing.
        /// </summary>
        /// <returns>The exception.</returns>
        public static VonageAuthenticationException FromMissingApiKeyOrSecret() =>
            new("API Key or API Secret missing.");

        /// <summary>
        ///     Creates an exception indicating the ApplicationId or PrivateKeyPath are missing.
        /// </summary>
        /// <returns>The exception.</returns>
        public static VonageAuthenticationException FromMissingApplicationIdOrPrivateKeyPath() =>
            new("AppId or Private Key Path missing.");
    }
}