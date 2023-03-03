namespace Vonage.Messages.Viber;

/// <summary>
///     Represents a Viber message to be sent by an IMessageClient.
/// </summary>
public interface IViberMessage : IMessage
{
    /// <summary>
    ///     Gets or sets Viber-specific information.
    /// </summary>
    ViberRequestData Data { get; set; }
}