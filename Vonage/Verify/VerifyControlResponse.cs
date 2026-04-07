using Newtonsoft.Json;

namespace Vonage.Verify;

/// <summary>
///     Represents the response from a verification control command, confirming the command that was executed.
/// </summary>
public class VerifyControlResponse : VerifyResponseBase
{
    /// <summary>
    ///     The control command that was executed ("cancel" or "trigger_next_event").
    /// </summary>
    [JsonProperty("command")]
    public string Command { get; set; }
}