using System.Runtime.Serialization;

namespace Vonage.Messaging;

/// <summary>
/// Represents the type of message.
/// </summary>
public enum SmsType
{
    /// <summary>
    /// 
    /// </summary>
    [EnumMember(Value = "text")]
    Text = 1,
        
    /// <summary>
    /// 
    /// </summary>
    [EnumMember(Value = "binary")]
    Binary = 2,
        
    /// <summary>
    /// 
    /// </summary>
    [EnumMember(Value = "unicode")]
    Unicode = 4,
}