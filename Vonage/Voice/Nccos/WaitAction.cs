#region
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.Nccos;

/// <summary>
/// </summary>
public class WaitAction : NccoAction
{
    /// <inheritdoc />
    public override ActionType Action => ActionType.Wait;

    /// <summary>
    ///     Controls the duration of the wait period before executing the next action in the NCCO. The range of possible values
    ///     is between 2 and 60 seconds. The default value is 10.
    /// </summary>
    [JsonProperty("timeout", Order = 1)]
    public int Timeout { get; set; } = 10;
}