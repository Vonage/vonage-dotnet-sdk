#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Mms;

/// <summary>
/// </summary>
public abstract class MmsMessageBase : MessageRequestBase
{
    /// <summary>
    ///     Allows to skip fraud checks on a per-message basis. The feature is feature-flagged and must be enabled for the api
    ///     key.
    /// </summary>
    [JsonPropertyOrder(99)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool TrustedNumber { get; set; }
}