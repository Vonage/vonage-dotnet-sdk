using System;
using System.Collections.Generic;
using System.Text;

namespace Nexmo.Api.Messaging
{
    [System.Obsolete("The Nexmo.Api.Messaging.SmsType enum is obsolete. " +
        "References to it should be switched to the new Vonage.Messaging.SmsType enum.")]
    public enum SmsType
    {
        text = 1,
        binary = 2,
        wappush = 3,
        unicode = 4,
        vcal = 5,
        vcar = 6
    }
}
