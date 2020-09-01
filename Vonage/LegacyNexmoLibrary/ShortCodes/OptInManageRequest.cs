using Newtonsoft.Json;

namespace Nexmo.Api.ShortCodes
{
    [System.Obsolete("The Nexmo.Api.ShortCodes.OptInManageRequest class is obsolete. " +
        "References to it should be switched to the new Vonage.ShortCodes.OptInManageRequest class.")]
    public class OptInManageRequest
    {
        [JsonProperty("msisdn")]
        public string Msisdn { get; set; }
    }
}