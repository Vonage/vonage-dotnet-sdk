using Newtonsoft.Json;

namespace Nexmo.Api.ShortCodes
{
    public class OptInManageRequest
    {
        [JsonProperty("msisdn")]
        public string Msisdn { get; set; }
    }
}