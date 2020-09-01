using System;
using System.Collections.Generic;
using System.Text;

namespace Vonage.NumberInsights
{
    public enum NumberReachability
    {
        unknown,
        reachable,
        undeliverable,
        absent,
        bad_number,
        blacklisted
    }
}
