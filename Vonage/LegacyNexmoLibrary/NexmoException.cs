using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api
{
    [System.Obsolete("The Nexmo.Api.NexmoException class is obsolete.")]
    public class NexmoException : Exception
    {
        public NexmoException(string message) : base(message) { }
    }
}
