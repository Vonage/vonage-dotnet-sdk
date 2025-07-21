#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Rcs;

/// <summary>
/// </summary>
public abstract class RcsMessageBase : MessageRequestBase
{
    /// <summary>
    /// </summary>
    [JsonPropertyName("rcs")]
    [JsonPropertyOrder(90)]
    public MessageRcs? Rcs { get; set; }
}