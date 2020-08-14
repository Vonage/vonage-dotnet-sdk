using System;
using System.Collections.Generic;
using System.Text;

namespace Vonage.Messaging
{
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
