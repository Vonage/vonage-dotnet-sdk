using Vonage.Common;
using Vonage.Common.Serialization;
using Vonage.Messages;

namespace Vonage.Serialization;

/// <summary>
///     Builder for Video serializer.
/// </summary>
public static class JsonSerializerBuilder
{
    /// <summary>
    ///     Build a serializer.
    /// </summary>
    /// <returns>A serializer.</returns>
    public static JsonSerializer Build() => JsonSerializer.BuildWithSnakeCase()
        .WithConverter(new EnumDescriptionJsonConverter<MessagesChannel>())
        .WithConverter(new EnumDescriptionJsonConverter<MessagesMessageType>());
}