using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Conversations
{
    public class Channel
    {
        public enum ChannelType 
        {
            app = 1
        }
        public ChannelType Type { get; set; }
    }
}
