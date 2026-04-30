using System.Text.Json.Serialization;

namespace Vonage.ApplicationsNew.ListApplications;

/// <summary>
///     Represents a paginated list of Vonage applications.
/// </summary>
public record ListApplicationsResponse(
    [property: JsonPropertyName("page_size")] int PageSize,
    [property: JsonPropertyName("page")] int Page,
    [property: JsonPropertyName("total_items")] int TotalItems,
    [property: JsonPropertyName("total_pages")] int TotalPages,
    [property: JsonPropertyName("_embedded")] ListApplicationsEmbedded Embedded);

/// <summary>
///     Represents the embedded applications payload within a <see cref="ListApplicationsResponse"/>.
/// </summary>
public record ListApplicationsEmbedded(
    [property: JsonPropertyName("applications")] ApplicationResponse[] Applications);
