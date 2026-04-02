using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vonage.Numbers;

/// <summary>
///     Represents the response from a number search operation, containing a paginated list of numbers.
/// </summary>
public class NumbersSearchResponse
{
    /// <summary>
    /// The total amount of numbers available in the pool.
    /// </summary>
    [JsonProperty("count")]
    public int Count { get; set; }

    /// <summary>
    /// A paginated array of available numbers and their details
    /// </summary>
    [JsonProperty("numbers")]
    public IList<Number> Numbers { get; set; }
}