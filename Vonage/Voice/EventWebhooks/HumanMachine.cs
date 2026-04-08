#region
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
///     Webhook event received when advanced machine detection determines whether a call was answered by a human or a machine (voicemail). The status will be either "human" or "machine".
/// </summary>
public class HumanMachine : CallStatusEvent
{
    /// <summary>
    ///     The unique identifier for this call. Note: this event uses <c>call_uuid</c> instead of the standard <c>uuid</c> property.
    /// </summary>
    [JsonProperty("call_uuid")]
    [JsonPropertyName("call_uuid")]
    public override string Uuid { get; set; }
}