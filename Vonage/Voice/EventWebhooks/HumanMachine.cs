#region
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
/// </summary>
public class HumanMachine : CallStatusEvent
{
    /// <summary>
    /// The unique identifier for this call (Note call_uuid, not uuid as in some other endpoints)
    /// </summary>
    [JsonProperty("call_uuid")]
    [JsonPropertyName("call_uuid")]
    public override string Uuid { get; set; }
}