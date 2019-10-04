using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Request
{
    public class UpdateConversationRequest : BaseConversationRequest
    {
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
    }
}
