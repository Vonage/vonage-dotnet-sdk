using Newtonsoft.Json;
using Nexmo.Api.Applications.Capabilities;

namespace Nexmo.Api.Applications
{
    [System.Obsolete("The Nexmo.Api.Applications.Application class is obsolete. " +
        "References to it should be switched to the new Vonage.Applications.Application class.")]
    public class Application
    {
        /// <summary>
        /// The application's ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Friendly identifier for your application. This is not unique
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Configuration for the products available in this application
        /// </summary>
        [JsonProperty("capabilities")]
        public ApplicationCapabilities Capabilities { get; set; }

        /// <summary>
        /// The keys for your application
        /// </summary>
        [JsonProperty("keys")]
        public Keys Keys { get; set; }
    }
}