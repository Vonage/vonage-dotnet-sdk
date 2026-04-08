using Newtonsoft.Json;
using Vonage.Voice.Nccos;

namespace Vonage.Voice;

/// <summary>
///     Represents the transfer destination for modifying an in-progress call. The destination can be either an inline NCCO or an answer URL that returns an NCCO.
/// </summary>
public class Destination
{
    /// <summary>
    ///     The destination type. Always "ncco".
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; } = "ncco";

    /// <summary>
    ///     The URL that Vonage makes a request to, which must return an NCCO. Used for transfer via answer URL.
    /// </summary>
    [JsonProperty("url")]
    public string[] Url { get; set; }

    /// <summary>
    ///     An inline NCCO to transfer the call to. Used for transfer via inline NCCO.
    /// </summary>
    [JsonProperty("ncco")]
    public Ncco Ncco { get; set; }
}