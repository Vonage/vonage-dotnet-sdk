using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Common;

/// <summary>
///     Provides serialization features to/from JSON.
/// </summary>
public class JsonSerializer : IJsonSerializer
{
    private readonly JsonSerializerSettings settings;

    public JsonSerializer()
    {
        this.settings = new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()};
    }

    /// <inheritdoc />
    public string SerializeObject<T>(T value) => JsonConvert.SerializeObject(value, this.settings);

    /// <inheritdoc />
    public Result<T> DeserializeObject<T>(string serializedValue)
    {
        try
        {
            var serializedObject = JsonConvert.DeserializeObject<T>(serializedValue);
            return Result<T>.FromSuccess(serializedObject);
        }
        catch (Exception)
        {
            return Result<T>.FromFailure(
                ResultFailure.FromErrorMessage($"Unable to deserialize '{serializedValue}' into '{typeof(T).Name}'."));
        }
    }
}