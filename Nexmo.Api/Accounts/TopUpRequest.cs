using Newtonsoft.Json;

namespace Nexmo.Api.Accounts
{
    public class TopUpRequest
    {
        /// <summary>
        /// The transaction reference of the transaction when balance was added and auto-reload was enabled on your account.
        /// </summary>
        [JsonProperty("trx")]
        public string Trx { get; set; }
    }
}