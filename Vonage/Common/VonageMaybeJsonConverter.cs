using Vonage.Common.Serialization;
using Vonage.Serialization;

namespace Vonage.Common;

/// <summary>
///     Represents a custom converter from Maybe to Json, using specific conversion for Vonage entities.
/// </summary>
/// <typeparam name="T">The underlying type.</typeparam>
public class VonageMaybeJsonConverter<T> : MaybeJsonConverter<T>
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    public VonageMaybeJsonConverter() => this.Serializer = JsonSerializerBuilder.BuildWithSnakeCase();
}