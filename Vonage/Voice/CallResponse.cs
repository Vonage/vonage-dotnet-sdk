using Newtonsoft.Json;

namespace Vonage.Voice;

/// <summary>
///     Represents the response from creating an outbound call via the Voice API.
/// </summary>
public class CallResponse
{
    /// <summary>
    ///     The unique identifier for the conversation this call is part of.
    /// </summary>
    [JsonProperty("conversation_uuid")]
    public string ConversationUuid { get; set; }

    /// <summary>
    ///     The direction of the call: "outbound" or "inbound".
    /// </summary>
    [JsonProperty("direction")]
    public string Direction { get; set; }

    /// <summary>
    ///     The initial status of the call: started, ringing, answered, machine, timeout, completed, busy, cancelled, failed, rejected, or unanswered.
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    ///     The unique identifier (UUID) for this call leg. Use this in subsequent API calls to control the call.
    /// </summary>
    [JsonProperty("uuid")]
    public string Uuid { get; set; }
}