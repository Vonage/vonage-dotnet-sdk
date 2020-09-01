using Newtonsoft.Json;

namespace Nexmo.Api.Accounts
{
    [System.Obsolete("The Nexmo.Api.Accounts.CreateSecretRequest class is obsolete. " +
        "References to it should be switched to the new Vonage.Accounts.CreateSecretRequest class.")]
    public class CreateSecretRequest
    {
        /// <summary>
        /// the new secret to be created
        /// The new secret must follow these rules:
        ///   minimum 8 characters
        ///   maximum 25 characters
        ///   minimum 1 lower case character
        ///   minimum 1 upper case character
        ///   minimum 1 digit
        /// </summary>
        [JsonProperty("secret")]
        public string Secret { get; set; }
    }
}