using Newtonsoft.Json;

namespace Nexmo.Api.Accounts
{
    [System.Obsolete("The Nexmo.Api.Accounts.TopUpResult class is obsolete. " +
        "References to it should be switched to the new Vonage.Accounts.TopUpResult class.")]
    public class TopUpResult
    {
        [JsonProperty("response")]
        public string Response { get; set; }
    }
}