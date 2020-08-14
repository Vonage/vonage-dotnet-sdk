using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nexmo.Api.Numbers
{
    [System.Obsolete("The Nexmo.Api.Numbers.NumbersSearchResponse class is obsolete. " +
        "References to it should be switched to the new Vonage.Numbers.NumbersSearchResponse class.")]
    public class NumbersSearchResponse
    {
        /// <summary>
        /// The total amount of numbers available in the pool.
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; set; }

        /// <summary>
        /// A paginated array of available numbers and their details
        /// </summary>
        [JsonProperty("numbers")]
        public IList<Number> Numbers { get; set; }
    }
}