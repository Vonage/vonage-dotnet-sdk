using Newtonsoft.Json;

namespace Vonage.Voice;

/// <summary>
///     Represents a command to send DTMF (Dual-Tone Multi-Frequency) tones into an active call.
/// </summary>
public class DtmfCommand
{
    /// <summary>
    ///     The DTMF digit string to send to the call (e.g., "1713", "p*123#"). Supports digits 0-9, *, #, and p for 500ms pauses.
    /// </summary>
    [JsonProperty("digits")]
    public string Digits { get; set; }
}