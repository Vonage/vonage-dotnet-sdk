#region
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Rcs;

/// <summary>
///     Represents a custom message request to be sent via RCS with a user-defined payload.
/// </summary>
public class RcsCustomRequest : RcsMessageBase
{
    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.RCS;

    /// <inheritdoc />
    public override IEnumerable<string> GetErrors() =>
        base.GetErrors().Concat(this.ValidateCustom());

    private IEnumerable<string> ValidateCustom()
    {
        if (this.Custom == null)
            yield return "Custom must not be null.";
    }

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.Custom;

    /// <summary>
    ///     A custom payload. The schema of a custom object can vary widely.
    /// </summary>
    [JsonPropertyName("custom")]
    [JsonPropertyOrder(9)]
    public object Custom { get; set; }
}