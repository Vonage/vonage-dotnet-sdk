using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nexmo.Api.Voice
{
    [System.Obsolete("The Nexmo.Api.Voice.CallList class is obsolete. " +
           "References to it should be switched to the new Nexmo.Api.Voice.CallList class.")]
    public class CallList
    {
        [JsonProperty("calls")]
        public List<CallRecord> Calls { get; set; }
    }
}
