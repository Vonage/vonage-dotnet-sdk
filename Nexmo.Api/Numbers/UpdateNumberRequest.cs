using System;
using Newtonsoft.Json;

namespace Nexmo.Api.Numbers
{
    public class UpdateNumberRequest
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("msisdn")]
        public string Msisdn { get; set; }

        [JsonProperty("app_id")]
        public string AppId { get; set; }

        [JsonProperty("moHttpUrl")]
        public string MoHttpUrl { get; set; }

        [JsonProperty("moSmppSysType")]
        public string MoSmppSysType { get; set; }

        [JsonProperty("voiceCallbackType")]
        public string VoiceCallbackType { get; set; }

        [JsonProperty("voiceCallbackValue")]
        public string VoiceCallbackValue { get; set; }

        [JsonProperty("voiceStatusCallback")]
        public string VoiceStatusCallback { get; set; }
    }
}