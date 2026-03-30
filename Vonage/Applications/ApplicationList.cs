#region
using System.Collections.Generic;
using Newtonsoft.Json;
#endregion

namespace Vonage.Applications;

/// <summary>
///     Represents a collection of applications returned from the API.
/// </summary>
public class ApplicationList
{
    /// <summary>
    ///     The list of applications.
    /// </summary>
    [JsonProperty("applications")]
    public IList<Application> Applications { get; set; }
}