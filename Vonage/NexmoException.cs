using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vonage
{
    public class VonageException : Exception
    {
        public VonageException(string message) : base(message) { }
    }
}
