using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vonage.Voice;

public class CallList
{
    [JsonProperty("calls")] public List<CallRecord> Calls { get; set; }
}