using System;
using Newtonsoft.Json;

namespace Vonage.Video.Beta.Common;

public class JsonSerializer : IJsonSerializer
{
    /// <summary>
    ///     Serializes the specified object to a JSON string.
    /// </summary>
    /// <param name="value">The object to serialize.</param>
    /// <typeparam name="T">The object type.</typeparam>
    /// <returns>The serialized string.</returns>
    public string SerializeObject<T>(T value) => JsonConvert.SerializeObject(value);

    /// <summary>
    ///     Deserializes the JSON to the specified type.
    /// </summary>
    /// <param name="serializedValue">The JSON to deserialize.</param>
    /// <typeparam name="T">The object type.</typeparam>
    /// <returns>Success if deserialization succeeded, Failure otherwise.</returns>
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