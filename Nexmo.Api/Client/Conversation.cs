using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Client
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
