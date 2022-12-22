﻿using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Common;

/// <summary>
///     Provides serialization features to/from JSON.
/// </summary>
public interface IJsonSerializer
{
    /// <summary>
    ///     Serializes the specified object to a JSON string.
    /// </summary>
    /// <param name="value">The object to serialize.</param>
    /// <typeparam name="T">The object type.</typeparam>
    /// <returns>The serialized string.</returns>
    string SerializeObject<T>(T value);

    /// <summary>
    ///     Deserializes the JSON to the specified type.
    /// </summary>
    /// <param name="serializedValue">The JSON to deserialize.</param>
    /// <typeparam name="T">The object type.</typeparam>
    /// <returns>Success if deserialization succeeded, Failure otherwise.</returns>
    Result<T> DeserializeObject<T>(string serializedValue);
}