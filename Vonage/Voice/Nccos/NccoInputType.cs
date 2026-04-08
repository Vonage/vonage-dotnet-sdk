using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vonage.Voice.Nccos;

/// <summary>
///     Defines the input types that can be collected from a caller during an NCCO Input action.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum NccoInputType
{
    /// <summary>
    ///     Collect input as DTMF (Dual-Tone Multi-Frequency) keypad tones. The caller presses keys on their phone keypad.
    /// </summary>
    [EnumMember(Value = "dtmf")]
    DTMF = 0,

    /// <summary>
    ///     Collect input as spoken words using automatic speech recognition (ASR).
    /// </summary>
    [EnumMember(Value = "speech")]
    Speech = 1,
}