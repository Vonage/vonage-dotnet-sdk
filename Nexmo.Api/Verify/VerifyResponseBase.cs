using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Verify
{
    public abstract class VerifyResponseBase
    {

        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("error_text")]
        public string ErrorText { get; set; }
    }
}
