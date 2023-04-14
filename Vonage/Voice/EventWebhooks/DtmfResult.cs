using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vonage.Voice.EventWebhooks;

public class DtmfResult
{
    /// <summary>
    /// the dtmf digits input by the user
    /// </summary>
    [JsonProperty("digits")]
    public string Digits { get; set; }

    /// <summary>
    /// indicates whether the dtmf input timed out
    /// </summary>
    [JsonProperty("timed_out")]
    public bool TimedOut { get; set; }

}