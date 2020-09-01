using Newtonsoft.Json;

namespace Vonage.Accounts
{
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