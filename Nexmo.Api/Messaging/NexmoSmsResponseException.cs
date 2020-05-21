using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Messaging
{
    public class NexmoSmsResponseException : NexmoException
    {
        public NexmoSmsResponseException(string message) : base(message) { }

        public SendSmsResponse Response { get; set; }
    }
}
