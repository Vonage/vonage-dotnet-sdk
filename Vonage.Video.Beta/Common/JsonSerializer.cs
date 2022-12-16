using System;
using Newtonsoft.Json;

namespace Vonage.Video.Beta.Common;

/// <summary>
///     Provides serialization features to/from JSON.
/// </summary>
public class JsonSerializer : IJsonSerializer
{
    /// <inheritdoc />
    public string SerializeObject<T>(T value) => JsonConvert.SerializeObject(value);

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