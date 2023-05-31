using Newtonsoft.Json;

namespace Vonage.Accounts;

public class Embedded
{
    [JsonProperty("primary_account")]
    public PrimaryAccount PrimaryAccount { get; set; }

    [JsonProperty("subaccounts")]
    public SubAccount[] SubAccount { get; set; }
}
