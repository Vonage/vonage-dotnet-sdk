using Newtonsoft.Json;
using System.Collections.Generic;
namespace Nexmo.Api.Applications
{
    [System.Obsolete("The Nexmo.Api.Applications.ApplicationList class is obsolete. " +
        "References to it should be switched to the new Vonage.Applications.ApplicationList class.")]
    public class ApplicationList
    {
        [JsonProperty("applications")]
        public IList<Application> Applications { get; set; }
    }
}