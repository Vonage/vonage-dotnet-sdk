#region
using Newtonsoft.Json.Converters;
#endregion

namespace Vonage.Common;

/// <summary>
///     Custom JSON converter for DateTime values in paginated list responses.
///     Formats dates using ISO 8601 format: yyyy-MM-ddTHH:mm:ssZ.
/// </summary>
/// <remarks>
///     This converter is used internally for deserializing date fields in paginated API responses.
/// </remarks>
public class PageListDateTimeConverter : IsoDateTimeConverter
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PageListDateTimeConverter" /> class
    ///     with the ISO 8601 date format (yyyy-MM-ddTHH:mm:ssZ).
    /// </summary>
    public PageListDateTimeConverter()
    {
        this.DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ssZ";
    }
}