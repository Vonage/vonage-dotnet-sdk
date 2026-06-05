namespace Vonage.Messages.Mms;

/// <summary>
///     An object of optional settings for the MMS message.
/// </summary>
public class MmsMessageObject
{
    /// <summary>
    ///     An array of phone numbers in E.164 format representing the additional participants of the group MMS conversation.
    /// </summary>
    public string[] Participants { get; set; }
}
