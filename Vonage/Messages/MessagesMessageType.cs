#region
using System.ComponentModel;
using System.Runtime.Serialization;
#endregion

namespace Vonage.Messages;

/// <summary>
/// Defines the message type on a request.
/// </summary>
public enum MessagesMessageType
{
    /// <summary>
    /// Text.
    /// </summary>
    [EnumMember(Value = "text")] [Description("text")]
    Text = 0,

    /// <summary>
    /// Image.
    /// </summary>
    [EnumMember(Value = "image")] [Description("image")]
    Image = 1,

    /// <summary>
    /// Vcard.
    /// </summary>
    [EnumMember(Value = "vcard")] [Description("vcard")]
    Vcard = 2,

    /// <summary>
    /// Audio.
    /// </summary>
    [EnumMember(Value = "audio")] [Description("audio")]
    Audio = 3,

    /// <summary>
    /// Video.
    /// </summary>
    [EnumMember(Value = "video")] [Description("video")]
    Video = 4,

    /// <summary>
    /// File.
    /// </summary>
    [EnumMember(Value = "file")] [Description("file")]
    File = 5,

    /// <summary>
    /// Custom.
    /// </summary>
    [EnumMember(Value = "custom")] [Description("custom")]
    Custom = 6,

    /// <summary>
    /// Template.
    /// </summary>
    [EnumMember(Value = "template")] [Description("template")]
    Template = 7,

    /// <summary>
    /// Sticker.
    /// </summary>
    [EnumMember(Value = "sticker")] [Description("sticker")]
    Sticker = 8,

    /// <summary>
    ///     Content.
    /// </summary>
    [EnumMember(Value = "content")] [Description("content")]
    Content = 9,

    /// <summary>
    ///     File.
    /// </summary>
    [EnumMember(Value = "card")] [Description("card")]
    Card = 10,
}