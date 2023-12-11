using System.Text.Json;
using Vonage.Common.Serialization;
using Vonage.Messages;
using Vonage.NumberInsightV2.FraudCheck;
using Vonage.Server;
using Vonage.Video.Broadcast;
using JsonSerializer = Vonage.Common.JsonSerializer;

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
        .WithConverter(new EnumDescriptionJsonConverter<MessagesMessageType>())
        .WithConverter(new EnumDescriptionJsonConverter<FraudScoreLabel>())
        .WithConverter(new EnumDescriptionJsonConverter<LayoutType>())
        .WithConverter(new EnumDescriptionJsonConverter<RenderResolution>())
        .WithConverter(new EnumDescriptionJsonConverter<OutputMode>())
        .WithConverter(new EnumDescriptionJsonConverter<StreamMode>())
        .WithConverter(new EnumDescriptionJsonConverter<Broadcast.BroadcastStatus>())
        .WithConverter(new EnumDescriptionJsonConverter<Broadcast.RtmpStatus>());

    /// <summary>
    ///     Build a serializer.
    /// </summary>
    /// <returns>A serializer.</returns>
    public static JsonSerializer Build(JsonNamingPolicy policy) => new JsonSerializer(policy)
        .WithConverter(new EnumDescriptionJsonConverter<MessagesChannel>())
        .WithConverter(new EnumDescriptionJsonConverter<MessagesMessageType>())
        .WithConverter(new EnumDescriptionJsonConverter<FraudScoreLabel>())
        .WithConverter(new EnumDescriptionJsonConverter<LayoutType>())
        .WithConverter(new EnumDescriptionJsonConverter<RenderResolution>())
        .WithConverter(new EnumDescriptionJsonConverter<OutputMode>())
        .WithConverter(new EnumDescriptionJsonConverter<StreamMode>())
        .WithConverter(new EnumDescriptionJsonConverter<Broadcast.BroadcastStatus>())
        .WithConverter(new EnumDescriptionJsonConverter<Broadcast.RtmpStatus>());
}