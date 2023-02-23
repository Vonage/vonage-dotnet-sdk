using Vonage.Common;
using Vonage.Common.Serialization;
using Vonage.Server.Common;
using Vonage.Server.Video.Broadcast.Common;

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
    public static JsonSerializer Build() => new JsonSerializer()
        .WithConverter(new EnumDescriptionJsonConverter<LayoutType>())
        .WithConverter(new EnumDescriptionJsonConverter<RenderResolution>())
        .WithConverter(new EnumDescriptionJsonConverter<OutputMode>())
        .WithConverter(new EnumDescriptionJsonConverter<StreamMode>())
        .WithConverter(new EnumDescriptionJsonConverter<Broadcast.BroadcastStatus>())
        .WithConverter(new EnumDescriptionJsonConverter<Broadcast.RtmpStatus>());
}