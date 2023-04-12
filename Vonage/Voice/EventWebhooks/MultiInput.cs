using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vonage.Voice.EventWebhooks;

public class MultiInput : Input
{
    /// <summary>
    /// Result of Dtmf input
    /// </summary>
    [JsonProperty("dtmf")]
    new public DtmfResult Dtmf { get; set; }

    /// <summary>
    /// Result of the speech recognition
    /// </summary>
    [JsonProperty("speech")]
    public SpeechResult Speech { get; set; }
}