using Newtonsoft.Json;

namespace Nexmo.Api.Accounts
{
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