using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vonage.Voice;

/// <summary>
///     Represents the embedded list of calls returned from the Get Calls API response.
/// </summary>
public class CallList
{
    /// <summary>
    ///     The list of call records matching the search criteria.
    /// </summary>
    [JsonProperty("calls")] public List<CallRecord> Calls { get; set; }
}