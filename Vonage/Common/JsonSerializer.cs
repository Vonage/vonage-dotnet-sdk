#region
using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Server;
#endregion

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
        this.settings.Converters.Add(new PhoneNumberJsonConverter());
        this.settings.Converters.Add(new EmailJsonConverter());
        this.settings.Converters.Add(new EnumDescriptionJsonConverter<RenderResolution>());
    }

    /// <summary>
    ///     Constructor with specific naming policy.
    /// </summary>
    /// <param name="namingPolicy">The naming policy.</param>
    public JsonSerializer(JsonNamingPolicy namingPolicy) : this() =>
        this.settings.PropertyNamingPolicy = namingPolicy;

    public JsonSerializer(JsonSerializerOptions options) : this() =>
        this.settings = options;

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
}