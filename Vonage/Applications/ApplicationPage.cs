using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vonage.Applications;

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