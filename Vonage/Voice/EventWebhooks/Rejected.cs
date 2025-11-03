#region
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
/// </summary>
public class Rejected : CallStatusEvent
{
    /// <summary>
    ///     Value from CS sip:hangup event sent to Voice API.
    /// </summary>
    [JsonProperty("sip_code")]
    [JsonPropertyName("sip_code")]
    public int? SipCode { get; set; }
}