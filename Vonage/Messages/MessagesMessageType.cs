using System.Runtime.Serialization;

namespace Vonage.Messages
{
    public enum MessagesMessageType
    {
        [EnumMember(Value = "text")]
        Text = 0,

        [EnumMember(Value = "image")]
        Image = 1,

        [EnumMember(Value = "vcard")]
        Vcard = 2,

        [EnumMember(Value = "audio")]
        Audio = 3,

        [EnumMember(Value = "video")]
        Video = 4,

        [EnumMember(Value = "file")]
        File = 5,

        [EnumMember(Value = "custom")]
        Custom = 6,

        [EnumMember(Value = "template")]
        Template = 7
    }
}