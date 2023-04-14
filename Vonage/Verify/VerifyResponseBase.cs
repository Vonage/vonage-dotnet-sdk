using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vonage.Verify;

public abstract class VerifyResponseBase
{
    /// <summary>
    /// A value of 0 indicates that your user entered the correct code. If it is non-zero, check the error_text.
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// If the status is non-zero, this explains the error encountered.
    /// </summary>
    [JsonProperty("error_text")]
    public string ErrorText { get; set; }
}