using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Conversations
{
    public class CursorListParams
    {
        public uint page_size { get; set; }

        public string order { get; set; }

        public string cursor { get; set; }
    }

    public class EventListParams
    {
        public string start_id { get; set; }

        public string end_id { get; set; }
    }
}
