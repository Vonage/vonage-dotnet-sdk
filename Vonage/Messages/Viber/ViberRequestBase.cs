using Newtonsoft.Json;

namespace Vonage.Messages.Viber;

/// <summary>
///     Represents the base class for Viber requests.
/// </summary>
public abstract class ViberRequestBase : MessageRequestBase
{
    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.ViberService;

    /// <summary>
    /// </summary>
    [JsonProperty("viber_service")]
    public ViberRequestData Data { get; set; }
}