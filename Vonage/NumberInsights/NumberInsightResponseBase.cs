using Newtonsoft.Json;

namespace Vonage.NumberInsights;

/// <summary>
///     Base class for all Number Insight API responses containing common status and request tracking fields.
/// </summary>
public class NumberInsightResponseBase
{
    /// <summary>
    ///     The status code of the Number Insight request. A value of 0 indicates success.
    ///     Non-zero values indicate an error occurred during the lookup.
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; set; }

    /// <summary>
    ///     The unique identifier for this request, an alphanumeric string up to 40 characters.
    ///     Use this for troubleshooting and support inquiries.
    /// </summary>
    [JsonProperty("request_id")]
    public string RequestId { get; set; }
}