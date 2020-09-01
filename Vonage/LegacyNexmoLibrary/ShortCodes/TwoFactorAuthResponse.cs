using Newtonsoft.Json;
using Nexmo.Api.Common;

namespace Nexmo.Api.ShortCodes
{
    [System.Obsolete("The Nexmo.Api.ShortCodes.TwoFactorAuthResponse class is obsolete. " +
        "References to it should be switched to the new Vonage.ShortCodes.TwoFactorAuthResponse class.")]
    public class TwoFactorAuthResponse
    {
        [JsonProperty("message-count")]
        public string MessageCount { get; set; }

        
        public Message[] Messages { get; set; }
    }
}