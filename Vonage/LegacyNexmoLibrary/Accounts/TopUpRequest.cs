using Newtonsoft.Json;

namespace Nexmo.Api.Accounts
{
    [System.Obsolete("The Nexmo.Api.Accounts.TopUpRequest class is obsolete. " +
        "References to it should be switched to the new Vonage.Accounts.TopUpRequest class.")]
    public class TopUpRequest
    {
        /// <summary>
        /// The transaction reference of the transaction when balance was added and auto-reload was enabled on your account.
        /// </summary>
        [JsonProperty("trx")]
        public string Trx { get; set; }
    }
}