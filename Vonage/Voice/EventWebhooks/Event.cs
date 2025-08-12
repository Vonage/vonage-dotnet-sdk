#region
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
/// </summary>
public abstract class Event : EventBase
{
    /// <summary>
    /// The unique identifier for this call
    /// </summary>
    [JsonProperty("uuid")]
    [JsonPropertyName("uuid")]
    public virtual string Uuid { get; set; }
}