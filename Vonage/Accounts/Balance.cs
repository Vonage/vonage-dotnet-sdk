using Newtonsoft.Json;
namespace Vonage.Accounts
{
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