#region
using System;
using Newtonsoft.Json;
#endregion

namespace Vonage.NumberInsights;

public class AdvancedNumberInsightRequest : StandardNumberInsightRequest
{
    /// <summary>
    /// This parameter is deprecated as we are no longer able to retrieve reliable IP data globally from carriers.
    /// </summary>
    [JsonProperty("ip")]
    public string Ip { get; set; }

    [JsonProperty("real_time_data")]
    [Obsolete("Redundant property.")]
    public bool RealTimeData { get; set; }
}