using Newtonsoft.Json;

namespace Vonage.Voice.Nccos
{
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
