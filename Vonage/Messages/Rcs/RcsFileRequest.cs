#region
using System.Collections.Generic;
using System.Linq;
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
    public override IEnumerable<string> GetErrors() =>
        base.GetErrors().Concat(this.ValidateFile());

    private IEnumerable<string> ValidateFile()
    {
        if (this.File == null)
            yield return "File must not be null.";
        else if (string.IsNullOrEmpty(this.File.Url))
            yield return "File Url must not be null or empty.";
    }

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.File;

    /// <summary>
    ///     The file attachment. Supported types are .pdf
    /// </summary>
    [JsonPropertyName("file")]
    [JsonPropertyOrder(9)]
    public CaptionedAttachment File { get; set; }
}