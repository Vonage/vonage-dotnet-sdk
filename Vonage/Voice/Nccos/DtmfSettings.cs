using Newtonsoft.Json;

namespace Vonage.Voice.Nccos;

/// <summary>
///     Configures DTMF input collection settings for an NCCO Input action.
/// </summary>
public class DtmfSettings
{
    /// <summary>
    ///     The number of seconds of inactivity before the input is considered complete. The range is 1-10 seconds. Default is 3.
    /// </summary>
    [JsonProperty("timeOut")]
    public int? TimeOut { get; set; }

    /// <summary>
    ///     The maximum number of DTMF digits to collect. Range: 1-20. Default is 4.
    /// </summary>
    [JsonProperty("maxDigits")]
    public int? MaxDigits { get; set; }

    /// <summary>
    ///     Set to <c>true</c> to end input collection when the user presses #, even if fewer than <see cref="MaxDigits"/> digits were entered.
    /// </summary>
    [JsonProperty("submitOnHash")]
    public bool? SubmitOnHash { get; set; }
}