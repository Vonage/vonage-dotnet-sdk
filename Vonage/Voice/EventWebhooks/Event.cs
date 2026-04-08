#region
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
///     Abstract base class for Voice API webhook events that include a call leg UUID.
/// </summary>
public abstract class Event : EventBase
{
    /// <summary>
    ///     The unique identifier for this call leg.
    /// </summary>
    [JsonProperty("uuid")]
    [JsonPropertyName("uuid")]
    public virtual string Uuid { get; set; }
}