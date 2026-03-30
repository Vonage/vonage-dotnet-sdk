#region
using Vonage.Common.Monads;
#endregion

namespace Vonage.Common;

/// <summary>
///     Defines a contract for JSON serialization and deserialization with Result-based error handling.
/// </summary>
/// <remarks>
///     <para>Implementations use <see cref="Result{T}"/> for deserialization to handle failures gracefully without exceptions.</para>
///     <para>See <see cref="JsonSerializer"/> for the default implementation.</para>
/// </remarks>
public interface IJsonSerializer
{
    /// <summary>
    ///     Deserializes a JSON string to the specified type.
    /// </summary>
    /// <param name="serializedValue">The JSON string to deserialize.</param>
    /// <typeparam name="T">The target type to deserialize to.</typeparam>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing the deserialized object on success,
    ///     or a <see cref="Failures.DeserializationFailure"/> on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var result = serializer.DeserializeObject<User>("{\"name\":\"John\"}");
    /// result.Match(
    ///     user => Console.WriteLine(user.Name),
    ///     failure => Console.WriteLine("Deserialization failed")
    /// );
    /// ]]></code>
    /// </example>
    Result<T> DeserializeObject<T>(string serializedValue);

    /// <summary>
    ///     Serializes an object to a JSON string.
    /// </summary>
    /// <param name="value">The object to serialize.</param>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <returns>The JSON string representation of the object.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var json = serializer.SerializeObject(new User { Name = "John" });
    /// ]]></code>
    /// </example>
    string SerializeObject<T>(T value);
}