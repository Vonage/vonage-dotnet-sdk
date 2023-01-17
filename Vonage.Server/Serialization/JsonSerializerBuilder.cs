using System.Collections.Generic;
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Serialization;
using Vonage.Server.Video.Archives.Common;

namespace Vonage.Server.Serialization;

/// <summary>
///     Builder for Video serializer.
/// </summary>
public static class JsonSerializerBuilder
{
    /// <summary>
    ///     Build a serializer.
    /// </summary>
    /// <returns>A serializer.</returns>
    public static JsonSerializer Build() => new(new List<JsonConverter>
    {
        new EnumDescriptionJsonConverter<LayoutType>(),
        new EnumDescriptionJsonConverter<RenderResolution>(),
        new EnumDescriptionJsonConverter<OutputMode>(),
        new EnumDescriptionJsonConverter<StreamMode>(),
    });
}