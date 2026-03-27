#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Viber;

/// <summary>
///     Base class for all Viber Business Messages requests.
/// </summary>
public abstract class ViberMessageBase : MessageRequestBase
{
    /// <summary>
    ///     Gets or sets Viber-specific information.
    /// </summary>
    [JsonPropertyOrder(8)]
    [JsonPropertyName("viber_service")]
    public ViberRequestData Data { get; set; }
}