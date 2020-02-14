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

        /// <summary>
        /// Redact information from a Nexmo transaction
        /// </summary>
        /// <param name="redactRequest"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public NexmoResponse RedactTransaction (RedactRequest redactRequest, Credentials creds = null)
        {
            return Nexmo.Api.Redact.RedactTransaction(redactRequest, creds ?? Credentials);
        }
    }
}
