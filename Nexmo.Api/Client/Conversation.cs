using Nexmo.Api.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api
{
    public class Conversation
    {
        public Credentials Credentials { get; set; }

        public Conversation(Credentials creds)
        {
            Credentials = creds;
        }

    }
}
