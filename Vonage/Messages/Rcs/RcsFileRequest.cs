#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Rcs;

/// <summary>
///     Represents a file message request to be sent via RCS (Rich Communication Services). Supports PDF files.
/// </summary>
public class RcsFileRequest : RcsMessageBase
{
    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.RCS;

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.File;

    /// <summary>
    ///     The file attachment. Supported types are .pdf
    /// </summary>
    [JsonPropertyName("file")]
    [JsonPropertyOrder(9)]
    public CaptionedAttachment File { get; set; }
}