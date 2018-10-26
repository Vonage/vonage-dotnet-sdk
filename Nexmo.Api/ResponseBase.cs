using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Nexmo.Api
{
    public class ResponseBase
    {
        [JsonProperty("error-code")]
        [FromQuery(Name = "error-code")]
        public string ErrorCode { get; set; }
        [JsonProperty("error-code-label")]
        [FromQuery(Name = "error-code-label")]
        public string ErrorCodeLabel { get; set; }
    }
}
