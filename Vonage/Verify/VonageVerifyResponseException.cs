using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vonage.Verify
{
    public class VonageVerifyResponseException : VonageException
    {
        public VonageVerifyResponseException(string message) : base(message) { }

        public VerifyResponseBase Response {get;set;}
    }
}
