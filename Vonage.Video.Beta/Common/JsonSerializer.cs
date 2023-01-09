using System;
using System.Text.Json;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Video.Archives.Common;

namespace Vonage.Video.Beta.Common;

/// <summary>
///     Provides serialization features to/from JSON.
/// </summary>
public class JsonSerializer : IJsonSerializer
{
    private readonly JsonSerializerOptions settings;

    /// <summary>
    /// </summary>
    public JsonSerializer()
    {
        this.settings = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        this.settings.Converters.Add(new LayoutTypeConverter());
        this.settings.Converters.Add(new RenderResolutionConverter());
    }

    /// <inheritdoc />
    public string SerializeObject<T>(T value) => System.Text.Json.JsonSerializer.Serialize(value, this.settings);

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
}