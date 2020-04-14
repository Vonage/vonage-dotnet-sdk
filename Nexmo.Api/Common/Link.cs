using Newtonsoft.Json;

namespace Nexmo.Api.Common
{
    public class Link
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }
}