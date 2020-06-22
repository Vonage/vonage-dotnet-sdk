using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api
{
    public class NexmoException : Exception
    {
        public NexmoException(string message) : base(message) { }
    }
}
