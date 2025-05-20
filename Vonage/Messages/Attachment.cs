#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages;

/// <summary>
/// </summary>
public class Attachment
{
    /// <summary>
    ///     The URL of the attachment.
    /// </summary>
    [JsonPropertyOrder(1)]
    public string Url { get; set; }

    /// <summary>
    ///     Additional text
    /// </summary>
    [JsonPropertyOrder(2)]
    public string Caption { get; set; }

    /// <summary>
    ///     The type of attachment (Optional).
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyOrder(0)]
    public string Type { get; set; }
}