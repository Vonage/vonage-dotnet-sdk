using Newtonsoft.Json;
namespace Nexmo.Api.Accounts
{
    public class Balance
    {
        [JsonProperty("value")]
        public decimal Value { get; set; }

        [JsonProperty("autoReload")]
        public bool AutoReload { get; set; }
    }
}