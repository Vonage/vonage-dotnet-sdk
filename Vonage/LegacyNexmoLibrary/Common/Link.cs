using Newtonsoft.Json;

namespace Nexmo.Api.Common
{
    [System.Obsolete("The Nexmo.Api.Common.Link class is obsolete. " +
        "References to it should be switched to the new Vonage.Common.Link class.")]
    public class Link
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }
}