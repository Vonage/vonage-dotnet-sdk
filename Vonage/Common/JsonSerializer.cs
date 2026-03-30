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
///     Provides JSON serialization and deserialization using System.Text.Json with custom converters for Vonage types.
/// </summary>
/// <remarks>
///     <para>Pre-configured with camelCase naming, relaxed JSON escaping, and null value handling.</para>
///     <para>Includes converters for <see cref="PhoneNumber"/>, <see cref="MailAddress"/>, and enum types.</para>
/// </remarks>
/// <example>
/// <code><![CDATA[
/// var serializer = new JsonSerializer();
///
/// // Serialize an object
/// var json = serializer.SerializeObject(new { Name = "Test", Value = 123 });
///
/// // Deserialize with result handling
/// var result = serializer.DeserializeObject<MyType>(json);
/// result.Match(
///     success => Console.WriteLine($"Parsed: {success}"),
///     failure => Console.WriteLine($"Failed: {failure.GetFailureMessage()}")
/// );
/// ]]></code>
/// </example>
public class JsonSerializer : IJsonSerializer
{
    internal readonly JsonSerializerOptions Settings;

    /// <summary>
    ///     Initializes a new instance of the <see cref="JsonSerializer"/> class with default settings.
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

    /// <summary>
    ///     Initializes a new instance of the <see cref="JsonSerializer"/> class with custom options.
    /// </summary>
    /// <param name="options">The JSON serializer options to use.</param>
    public JsonSerializer(JsonSerializerOptions options) : this() =>
        this.Settings = options;

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

    /// <summary>
    ///     Adds a custom JSON converter to this serializer instance.
    /// </summary>
    /// <param name="converter">The JSON converter to add.</param>
    /// <returns>This serializer instance for method chaining.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var serializer = new JsonSerializer()
    ///     .WithConverter(new CustomTypeConverter());
    /// ]]></code>
    /// </example>
    public JsonSerializer WithConverter(JsonConverter converter)
    {
        this.Settings.Converters.Add(converter);
        return this;
    }
}