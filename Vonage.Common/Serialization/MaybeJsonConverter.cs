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
    private readonly JsonSerializer serializer;

    public MaybeJsonConverter() => this.serializer = new JsonSerializer();

    /// <inheritdoc />
    public override Maybe<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        this.serializer
            .DeserializeObject<T>(reader.GetString())
            .Match(Maybe<T>.Some, _ => Maybe<T>.None);

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, Maybe<T> value, JsonSerializerOptions options) =>
        value
            .Map(some => this.serializer.SerializeObject(some))
            .IfSome(some => writer.WriteRawValue(some));
}