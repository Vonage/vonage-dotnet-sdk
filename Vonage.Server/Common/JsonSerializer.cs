using System;
using System.Text.Json;
using Vonage.Server.Common.Failures;
using Vonage.Server.Common.Monads;
using Vonage.Server.Common.Serialization;
using Vonage.Server.Video.Archives.Common;

namespace Vonage.Server.Common;

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
        this.settings.Converters.Add(new EnumDescriptionJsonConverter<LayoutType>());
        this.settings.Converters.Add(new EnumDescriptionJsonConverter<RenderResolution>());
        this.settings.Converters.Add(new EnumDescriptionJsonConverter<OutputMode>());
        this.settings.Converters.Add(new EnumDescriptionJsonConverter<StreamMode>());
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
            return Result<T>.FromFailure(
                ResultFailure.FromErrorMessage($"Unable to deserialize '{serializedValue}' into '{typeof(T).Name}'."));
        }
    }

    /// <inheritdoc />
    public string SerializeObject<T>(T value) => System.Text.Json.JsonSerializer.Serialize(value, this.settings);
}