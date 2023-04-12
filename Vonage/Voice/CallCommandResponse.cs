using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vonage.Voice;

public class CallCommandResponse
{
    /// <summary>
    /// A string explaining the state of this request.
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; }
        
    /// <summary>
    /// The unique id for this request.
    /// </summary>
    [JsonProperty("uuid")]
    public string Uuid { get; set; }
}