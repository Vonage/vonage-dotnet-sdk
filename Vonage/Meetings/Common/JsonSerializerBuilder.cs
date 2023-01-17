using Vonage.Common;
using Yoh.Text.Json.NamingPolicies;

namespace Vonage.Meetings.Common;

/// <summary>
///     Builder for Video serializer.
/// </summary>
public static class JsonSerializerBuilder
{
    /// <summary>
    ///     Build a serializer.
    /// </summary>
    /// <returns>A serializer.</returns>
    public static JsonSerializer Build() => new(JsonNamingPolicies.SnakeCaseLower);
}