using Newtonsoft.Json;
using Vonage.Common;

namespace Vonage.Accounts
{
    public class SubAccount
    {
        /// <summary>
        /// the name of the subaccount
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// the API Key of the subaccount
        /// </summary>
        [JsonProperty("api_key")]
        public string ApiKey { get; set; }
        
        /// <summary>
        /// the secret of the subaccount
        /// </summary>
        [JsonProperty("secret")]
        public string Secret { get; set; }

        /// <summary>
        /// the primary API Key
        /// </summary>
        [JsonProperty("primary_account_api_key")]
        public string PrimaryAccountApiKey { get; set; }
        
        /// <summary>
        /// whether the subaccount should use the account balance of the primary account
        /// </summary>
        [JsonProperty("use_primary_account_balance")]
        public bool UsePrimaryAccountBalance { get; set; }
        
        /// <summary>
        /// the creation time of the subaccount
        /// </summary>
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; } 
        
        /// <summary>
        /// the suspension state of the subaccount
        /// </summary>
        [JsonProperty("suspended")]
        public bool Suspended { get; set; } 
        
        /// <summary>
        /// the current subaccount balance
        /// </summary>
        [JsonProperty("balance")]
        public double Balance { get; set; } 
        
        /// <summary>
        /// the credit limit of the subaccount
        /// </summary>
        [JsonProperty("credit_limit")]
        public double CreditLimit { get; set; } 
    }
}