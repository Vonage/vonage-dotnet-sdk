#region
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
///     Webhook event received when a call goes unanswered by the recipient.
/// </summary>
public class Unanswered : CallStatusEvent
{
    /// <summary>
    ///     Value from CS sip:hangup event sent to Voice API.
    /// </summary>
    [JsonProperty("sip_code")]
    [JsonPropertyName("sip_code")]
    public int? SipCode { get; set; }
}