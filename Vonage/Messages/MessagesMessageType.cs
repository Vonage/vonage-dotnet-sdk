using System.ComponentModel;
using System.Runtime.Serialization;

namespace Vonage.Messages;

/// <summary>
/// </summary>
public enum MessagesMessageType
{
    /// <summary>
    /// </summary>
    [EnumMember(Value = "text")] [Description("text")]
    Text = 0,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "image")] [Description("image")]
    Image = 1,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "vcard")] [Description("vcard")]
    Vcard = 2,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "audio")] [Description("audio")]
    Audio = 3,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "video")] [Description("video")]
    Video = 4,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "file")] [Description("file")]
    File = 5,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "custom")] [Description("custom")]
    Custom = 6,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "template")] [Description("template")]
    Template = 7,
}