using Newtonsoft.Json;

namespace Vonage.Applications;

public class ApplicationPage
{
    [JsonProperty("_embedded")] public ApplicationList Embedded { get; set; }

    [JsonProperty("page")] public int? Page { get; set; }

    [JsonProperty("page_size")] public int? PageSize { get; set; }

    [JsonProperty("total_items")] public int? TotalItems { get; set; }

    [JsonProperty("total_pages")] public int? TotalPages { get; set; }
}