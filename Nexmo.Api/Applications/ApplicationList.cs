using Newtonsoft.Json;
using System.Collections.Generic;
namespace Nexmo.Api.Applications
{
    public class ApplicationList
    {
        [JsonProperty("applications")]
        public IList<Application> Applications { get; set; }
    }
}