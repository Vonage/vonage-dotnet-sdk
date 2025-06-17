#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Viber;

public abstract class ViberMessageBase : MessageRequestBase
{
    /// <summary>
    ///     Gets or sets Viber-specific information.
    /// </summary>
    [JsonPropertyOrder(8)]
    [JsonPropertyName("viber_service")]
    public ViberRequestData Data { get; set; }
}