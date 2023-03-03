using System.ComponentModel;

namespace Vonage.Messages.Messenger;

public enum MessengerMessageCategory
{
    [Description("response")] Response = 0,

    [Description("update")] Update = 1,

    [Description("message_tag")] MessageTag = 2,
}