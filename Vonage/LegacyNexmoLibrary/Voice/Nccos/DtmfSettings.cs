using Newtonsoft.Json;

namespace Nexmo.Api.Voice.Nccos
{
    [System.Obsolete("The Nexmo.Api.Voice.Nccos.DtmfSettings class is obsolete. " +
           "References to it should be switched to the new Nexmo.Api.Voice.Nccos.DtmfSettings class.")]
    public class DtmfSettings
    {
        [JsonProperty("timeOut")]
        public int? TimeOut { get; set; }

        [JsonProperty("maxDigits")]
        public int? MaxDigits { get; set; }

        [JsonProperty("submitOnHash")]
        public bool? SubmitOnHash { get; set; }
    }
}
