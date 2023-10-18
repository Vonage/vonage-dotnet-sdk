using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Vonage.Common.Monads;

namespace Vonage.Common.Serialization;

/// <summary>
///     Represents a custom converter from Maybe to Json.
/// </summary>
/// <typeparam name="T">The underlying type.</typeparam>
public class MaybeJsonConverter<T> : JsonConverter<Maybe<T>>
{
    /// <inheritdoc />
    public override Maybe<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        this.Serializer.DeserializeObject<FakeJson>(BuildFakeJson(reader))
            .Map(result => result.Value)
            .Match(Maybe<T>.Some, _ => Maybe<T>.None);

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, Maybe<T> value, JsonSerializerOptions options) =>
        value
            .Map(some => this.Serializer.SerializeObject(some))
            .IfSome(some => writer.WriteRawValue(some));

    private static string BuildFakeJson(Utf8JsonReader reader) => "{\"value\":\"" + reader.GetString() + "\"}";

    private record FakeJson(T Value);

    protected JsonSerializer Serializer = new();
}