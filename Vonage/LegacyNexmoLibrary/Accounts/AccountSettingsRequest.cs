using Newtonsoft.Json;

namespace Nexmo.Api.Accounts
{
    [System.Obsolete("The Nexmo.Api.Accounts.AccountSettingsRequest class is obsolete. " +
        "References to it should be switched to the new Vonage.Accounts.AccountSettingsRequest class.")]
    public class AccountSettingsRequest
    {
        /// <summary>
        /// The URL where Nexmo will send a webhook when an SMS is received to a Nexmo number that does not have SMS
        /// handling configured. Send an empty string to unset this value.
        /// </summary>
        [JsonProperty("moCallBackUrl")]
        public string MoCallBackUrl { get; set; }
        
        /// <summary>
        /// The URL where Nexmo will send a webhook when an delivery receipt is received without a specific callback
        /// URL configured. Send an empty string to unset this value.
        /// </summary>
        [JsonProperty("drCallBackUrl")] 
        public string DrCallBackUrl { get; set; }
    }
}