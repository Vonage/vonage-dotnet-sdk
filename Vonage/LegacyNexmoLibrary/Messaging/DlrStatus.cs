using System;
using System.Collections.Generic;
using System.Text;

namespace Nexmo.Api.Messaging
{
    [System.Obsolete("The Nexmo.Api.Messaging.DlrStatus enum is obsolete. " +
        "References to it should be switched to the new Vonage.Messaging.DlrStatus enum.")]
    public enum DlrStatus
    {
        delivered = 0,
        expired = 1,
        failed = 2,
        rejected = 3,
        accepted = 4,
        buffered = 5,
        unknown = 6
    }
}
