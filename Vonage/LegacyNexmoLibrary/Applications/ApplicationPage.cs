using Newtonsoft.Json;

namespace Nexmo.Api.Applications
{
    [System.Obsolete("The Nexmo.Api.Applications.ApplicationPage class is obsolete. " +
        "References to it should be switched to the new Vonage.Applications.ApplicationPage class.")]
    public class ApplicationPage
    {
        [JsonProperty("page_size")]
        public int? PageSize { get; set; }

        [JsonProperty("page")]
        public int? Page { get; set; }

        [JsonProperty("total_items")]
        public int? TotalItems { get; set; }

        [JsonProperty("total_pages")]
        public int? TotalPages { get; set; }

        [JsonProperty("_embedded")]
        public ApplicationList Embedded { get; set; }
    }
}
