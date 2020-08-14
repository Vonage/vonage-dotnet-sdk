using System;
using Newtonsoft.Json;

namespace Nexmo.Api.ShortCodes
{
    [System.Obsolete("The Nexmo.Api.ShortCodes.AlertRequest class is obsolete. " +
        "References to it should be switched to the new Vonage.ShortCodes.AlertRequest class.")]
    public class AlertRequest
    {
        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("status-report-req")]
        public string StatusReportReq { get; set; }

        [JsonProperty("client-ref")]
        public string ClientRef { get; set; }

        [JsonProperty("template")]
        public string Template { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("custom")]
        public object Custom { get; set; }
    }
}