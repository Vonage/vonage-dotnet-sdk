using Newtonsoft.Json;

namespace Vonage.Accounts;

public class SubAccountsRequestResult
{
    [JsonProperty("links")]
    public Links Links { get; set; }


    [JsonProperty("total_balance")]
    public float TotalBlance { get; set; }


    [JsonProperty("total_credit_limit")]
    public float TotalCreditLimit { get; set; }


    [JsonProperty("_embedded")]
    public SubAccountList Embedded { get; set; }
}