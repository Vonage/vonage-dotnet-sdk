using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Request
{
    public class CreateMemberWithNameRequest :CreateMemberRequestBase
    {
        [JsonProperty("user_name")]
        public string Name { get; set; }
    }
}
