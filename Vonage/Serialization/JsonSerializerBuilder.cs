using System.Text.Json;
using Vonage.Common.Serialization;
using JsonSerializer = Vonage.Common.JsonSerializer;

namespace Vonage.Serialization;

/// <summary>
///     Builder for Video serializer.
/// </summary>
public static class JsonSerializerBuilder
{
    /// <summary>
    ///     Build a serializer with a Camel case policy.
    /// </summary>
    /// <returns>A serializer.</returns>
    public static JsonSerializer BuildWithCamelCase() => BuildSerializer(JsonNamingPolicy.CamelCase);
    
    /// <summary>
    ///     Build a serializer with a Snake case policy.
    /// </summary>
    /// <returns>A serializer.</returns>
    public static JsonSerializer BuildWithSnakeCase() => BuildSerializer(JsonNamingPolicy.SnakeCaseLower);
    
    private static JsonSerializer BuildSerializer(JsonNamingPolicy policy) => new JsonSerializer(policy)
        .WithConverter(new MaybeJsonConverter<string>())
        .WithConverter(new HttpMethodJsonConverter())
        .WithConverter(new PhoneNumberJsonConverter())
        .WithConverter(new EmailJsonConverter());
}