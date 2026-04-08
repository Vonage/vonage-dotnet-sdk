using Newtonsoft.Json;

namespace Vonage.Voice;

/// <summary>
///     Represents the response from an in-call command (DTMF, stream, or talk) confirming the action was performed.
/// </summary>
public class CallCommandResponse
{
    /// <summary>
    ///     A description of the action taken (e.g., "Talk started", "Stream stopped", "DTMF sent").
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; }

    /// <summary>
    ///     The UUID of the call leg the command was applied to.
    /// </summary>
    [JsonProperty("uuid")]
    public string Uuid { get; set; }
}