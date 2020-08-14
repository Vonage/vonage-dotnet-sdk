using Newtonsoft.Json;
using Nexmo.Api.Common;
namespace Nexmo.Api.ShortCodes
{
    [System.Obsolete("The Nexmo.Api.ShortCodes.AlertResponse class is obsolete. " +
        "References to it should be switched to the new Vonage.ShortCodes.AlertResponse class.")]
    public class AlertResponse
    {
        [JsonProperty("message-count")]
        public string MessageCount { get; set; }

        
        public Message[] Messages { get; set; }
    }
}