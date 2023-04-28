using System.Text.Json.Serialization;
using Vonage.Common;

namespace Vonage.ProactiveConnect;

/// <summary>
///     Represents a pagination result.
/// </summary>
/// <param name="Page">  The page number.</param>
/// <param name="PageSize">  The page size.</param>
/// <param name="TotalItems">   The number of total items.</param>
/// <param name="TotalPages">The number of total pages.</param>
/// <param name="Links">The HAL links.</param>
/// <param name="Embedded"> The embedded elements.</param>
/// <typeparam name="T">Type of embedded elements.</typeparam>
public record PaginationResult<T>(int Page, int PageSize, int TotalItems, int TotalPages,
    [property: JsonPropertyName("_links")] HalLinks Links, [property: JsonPropertyName("_embedded")]
    T Embedded);