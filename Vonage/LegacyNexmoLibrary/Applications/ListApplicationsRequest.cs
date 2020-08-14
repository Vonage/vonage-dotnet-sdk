using Newtonsoft.Json;

namespace Nexmo.Api.Applications
{
    [System.Obsolete("The Nexmo.Api.Applications.ListApplicationsRequest class is obsolete. " +
        "References to it should be switched to the new Vonage.Applications.ListApplicationsRequest class.")]
    public class ListApplicationsRequest
    {
        /// <summary>
        /// Number of records to retrieve per page
        /// </summary>
        [JsonProperty("page_size")]
        public int PageSize { get; set; }
        
        /// <summary>
        /// the page to retrieve
        /// </summary>
        [JsonProperty("page")]
        public int Page { get; set; }
    }
}