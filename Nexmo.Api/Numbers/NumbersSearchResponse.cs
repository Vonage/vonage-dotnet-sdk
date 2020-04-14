using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nexmo.Api.Numbers
{
    public class NumbersSearchResponse
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("numbers")]
        public IList<Number> Numbers { get; set; }
    }
}