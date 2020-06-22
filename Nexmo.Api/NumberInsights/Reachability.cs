using System;
using System.Collections.Generic;
using System.Text;

namespace Nexmo.Api.NumberInsights
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
