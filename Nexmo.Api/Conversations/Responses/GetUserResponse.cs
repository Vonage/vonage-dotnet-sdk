using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Conversations
{
    public class GetUserResponse
    {
        public class Links
        {
            public class SelfLink
            {
                [JsonProperty("href")]
                public string HRef { get; set; }
            }
        }
    }
}
