using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Request
{
    public class CreateMemberWithIdRequest : CreateMemberRequestBase
    {
        [JsonProperty("user_id")]
        public string Id { get; set; }
    }
}
