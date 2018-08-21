using Nexmo.Api.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Nexmo.Api.Redact;

namespace Nexmo.Api.ClientMethods
{
    public class Redact
    {
        public Credentials Credentials { get; set; }

        public Redact(Credentials creds)
        {
            Credentials = creds;
        }

        public void RedactTransaction (RedactRequest redactRequest, Credentials creds = null)
        {
            Nexmo.Api.Redact.RedactTransaction(redactRequest, creds ?? Credentials);
        }
    }
}
