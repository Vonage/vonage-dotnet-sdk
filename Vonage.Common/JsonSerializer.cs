using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Vonage.Common.Failures;
using Vonage.Common.Monads;

namespace Vonage.Common;

/// <summary>
///     Provides serialization features to/from JSON.
/// </summary>
public class JsonSerializer : IJsonSerializer
{
    private readonly JsonSerializerOptions settings;

    /// <summary>
    /// </summary>
    public JsonSerializer() =>
        this.settings = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

    /// <summary>
    /// </summary>
    public JsonSerializer(JsonNamingPolicy namingPolicy) : this() =>
        this.settings.PropertyNamingPolicy = namingPolicy;

    /// <summary>
    /// </summary>
    /// <param name="converters"></param>
    public JsonSerializer(IEnumerable<JsonConverter> converters)
        : this() =>
        converters.ToList().ForEach(converter => this.settings.Converters.Add(converter));

    /// <summary>
    /// </summary>
    /// <param name="converters"></param>
    /// <param name="namingPolicy"></param>
    public JsonSerializer(IEnumerable<JsonConverter> converters, JsonNamingPolicy namingPolicy)
        : this(namingPolicy) =>
        converters.ToList().ForEach(converter => this.settings.Converters.Add(converter));

    /// <inheritdoc />
    public Result<T> DeserializeObject<T>(string serializedValue)
    {
        try
        {
            var serializedObject = System.Text.Json.JsonSerializer.Deserialize<T>(serializedValue, this.settings);
            return Result<T>.FromSuccess(serializedObject);
        }
        catch (Exception)
        {
            return Result<T>.FromFailure(
                ResultFailure.FromErrorMessage($"Unable to deserialize '{serializedValue}' into '{typeof(T).Name}'."));
        }
    }

    /// <inheritdoc />
    public string SerializeObject<T>(T value) => System.Text.Json.JsonSerializer.Serialize(value, this.settings);
}