using Newtonsoft.Json;

namespace Vonage.Accounts;

public class CreateSubAccountRequest
{
    /// <summary>
    /// the new subaccount to be created
    ///   maximum 80 characters
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }
        
    [JsonProperty("secret")]
    public string Secret { get; set; }

    [JsonProperty("use_primary_account_balance")]
    public bool UsePrimaryAccountBalance { get; set; } = true;
}