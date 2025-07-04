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
    internal readonly JsonSerializerOptions Settings;

    /// <summary>
    ///     Default constructor.
    /// </summary>
    public JsonSerializer()
    {
        this.Settings = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };
        this.Settings.Converters.Add(new PhoneNumberJsonConverter());
        this.Settings.Converters.Add(new EmailJsonConverter());
        this.Settings.Converters.Add(new EnumDescriptionJsonConverter<RenderResolution>());
    }

    /// <summary>
    ///     Constructor with specific naming policy.
    /// </summary>
    /// <param name="namingPolicy">The naming policy.</param>
    public JsonSerializer(JsonNamingPolicy namingPolicy) : this() =>
        this.Settings.PropertyNamingPolicy = namingPolicy;

    public JsonSerializer(JsonSerializerOptions options) : this() =>
        this.Settings = options;

    /// <summary>
    ///     Add the specified converter to the current instance.
    /// </summary>
    /// <param name="converter">The converter.</param>
    /// <returns>The serializer.</returns>
    public JsonSerializer WithConverter(JsonConverter converter)
    {
        this.Settings.Converters.Add(converter);
        return this;
    }

    /// <inheritdoc />
    public Result<T> DeserializeObject<T>(string serializedValue)
    {
        try
        {
            var serializedObject = System.Text.Json.JsonSerializer.Deserialize<T>(serializedValue, this.Settings);
            return Result<T>.FromSuccess(serializedObject);
        }
        catch (Exception)
        {
            return Result<T>.FromFailure(DeserializationFailure.From(typeof(T), serializedValue));
        }
    }

    /// <inheritdoc />
    public string SerializeObject<T>(T value) => System.Text.Json.JsonSerializer.Serialize(value, this.Settings);
}