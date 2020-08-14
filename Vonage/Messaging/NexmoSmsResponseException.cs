using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vonage.Messaging
{
    public class VonageSmsResponseException : VonageException
    {
        public VonageSmsResponseException(string message) : base(message) { }

        public SendSmsResponse Response { get; set; }
    }
}
