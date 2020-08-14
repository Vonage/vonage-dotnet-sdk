using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vonage.Numbers
{
    public class VonageNumberResponseException : VonageException
    {
        public VonageNumberResponseException(string message) : base(message) {  }
        public NumberTransactionResponse Response { get; set; }
    }
}
