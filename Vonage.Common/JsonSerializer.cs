using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Yoh.Text.Json.NamingPolicies;

namespace Vonage.Common;

/// <summary>
///     Provides serialization features to/from JSON.
/// </summary>
public class JsonSerializer : IJsonSerializer
{
    private readonly JsonSerializerOptions settings;

    /// <summary>
    ///     Default constructor.
    /// </summary>
    public JsonSerializer()
    {
        this.settings = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };
        this.settings.Converters.Add(new ColorJsonConverter());
        this.settings.Converters.Add(new PhoneNumberJsonConverter());
        this.settings.Converters.Add(new EmailJsonConverter());
    }

    /// <summary>
    ///     Constructor with specific naming policy.
    /// </summary>
    /// <param name="namingPolicy">The naming policy.</param>
    public JsonSerializer(JsonNamingPolicy namingPolicy) : this() =>
        this.settings.PropertyNamingPolicy = namingPolicy;

    /// <summary>
    /// </summary>
    /// <param name="converters"></param>
    /// <param name="namingPolicy"></param>
    public JsonSerializer(IEnumerable<JsonConverter> converters, JsonNamingPolicy namingPolicy)
        : this(namingPolicy) =>
        converters.ToList().ForEach(converter => this.settings.Converters.Add(converter));

    /// <summary>
    /// Builds a serializer with SnakeCase naming policy.
    /// </summary>
    /// <returns>The serializer.</returns>
    public static JsonSerializer BuildWithCamelCase() => new(JsonNamingPolicy.CamelCase);

    /// <summary>
    ///     Builds a serializer with SnakeCase naming policy.
    /// </summary>
    /// <returns>The serializer.</returns>
    public static JsonSerializer BuildWithSnakeCase() => new(JsonNamingPolicies.SnakeCaseLower);

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
            return Result<T>.FromFailure(DeserializationFailure.From(typeof(T), serializedValue));
        }
    }

    /// <inheritdoc />
    public string SerializeObject<T>(T value) => System.Text.Json.JsonSerializer.Serialize(value, this.settings);

    /// <summary>
    ///     Add the specified converter to the current instance.
    /// </summary>
    /// <param name="converter">The converter.</param>
    /// <returns>The serializer.</returns>
    public JsonSerializer WithConverter(JsonConverter converter)
    {
        this.settings.Converters.Add(converter);
        return this;
    }
}