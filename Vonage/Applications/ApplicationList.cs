using Newtonsoft.Json;
using System.Collections.Generic;
namespace Vonage.Applications
{
    public class ApplicationList
    {
        [JsonProperty("applications")]
        public IList<Application> Applications { get; set; }
    }
}