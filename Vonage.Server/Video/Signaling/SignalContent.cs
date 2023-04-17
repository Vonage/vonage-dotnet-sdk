namespace Vonage.Server.Video.Signaling;

/// <summary>
///     Represents a signal to be sent.
/// </summary>
public readonly struct SignalContent
{
    /// <summary>
    ///     Payload that is being sent to the client. This cannot exceed 8kb.
    /// </summary>
    public string Data { get; }

    /// <summary>
    ///     Type of data that is being sent to the client. This cannot exceed 128 bytes.
    /// </summary>
    public string Type { get; }

    /// <summary>
    ///     Creates a signal.
    /// </summary>
    /// <param name="type">Type of data that is being sent to the client. This cannot exceed 128 bytes.</param>
    /// <param name="data">Payload that is being sent to the client. This cannot exceed 8kb.</param>
    public SignalContent(string type, string data)
    {
        this.Type = type;
        this.Data = data;
    }
}