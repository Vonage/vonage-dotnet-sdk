using Newtonsoft.Json;

namespace Vonage.Accounts;

public class SubAccountList
{
    [JsonProperty("primary_account")]
    public PrimaryAccount PrimaryAccount { get; set; }

    [JsonProperty("subaccounts")]
    public SubAccount[] SubAccount { get; set; }
}
