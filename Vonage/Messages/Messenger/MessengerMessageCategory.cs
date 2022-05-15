using System.Runtime.Serialization;

namespace Vonage.Messages.Messenger
{
    public enum MessengerMessageCategory
    {
        [EnumMember(Value = "response")]
        Response = 0,
        
        [EnumMember(Value = "update")] 
        Update = 1,
        
        [EnumMember(Value = "message_tag")]
        MessageTag = 2
    }
}