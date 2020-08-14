using Newtonsoft.Json;
namespace Nexmo.Api.Accounts
{
    [System.Obsolete("The Nexmo.Api.Accounts.Balance class is obsolete. " +
        "References to it should be switched to the new Vonage.Accounts.Balance class.")]
    public class Balance
    {
        /// <summary>
        /// The balance of the account, in EUR
        /// </summary>
        [JsonProperty("value")]
        public decimal Value { get; set; }

        /// <summary>
        /// Whether the account has auto-reloading enabled
        /// </summary>
        [JsonProperty("autoReload")]
        public bool AutoReload { get; set; }
    }
}