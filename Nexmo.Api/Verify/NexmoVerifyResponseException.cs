using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Verify
{
    public class NexmoVerifyResponseException : NexmoException
    {
        public NexmoVerifyResponseException(string message) : base(message) { }

        public VerifyResponseBase Response {get;set;}
    }
}
