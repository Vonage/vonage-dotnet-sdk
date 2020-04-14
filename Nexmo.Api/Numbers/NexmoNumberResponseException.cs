using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Numbers
{
    public class NexmoNumberResponseException : NexmoException
    {
        public NexmoNumberResponseException(string message) : base(message) {  }
    }
}
