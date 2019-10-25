using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nexmo.Api.Conversations
{
    public class EventList
    {
        [JsonProperty("events")]
        public IList<Event> Events { get; set; }
    }
}
