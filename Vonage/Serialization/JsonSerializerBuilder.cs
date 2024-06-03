using System.Text.Json;
using Vonage.Common.Serialization;
using Vonage.Conversations;
using Vonage.Messages;
using Vonage.NumberInsightV2.FraudCheck;
using Vonage.Server;
using Vonage.VerifyV2.StartVerification;
using Vonage.Video.Broadcast;
using JsonSerializer = Vonage.Common.JsonSerializer;

namespace Vonage.Serialization;

/// <summary>
///     Builder for Video serializer.
/// </summary>
public static class JsonSerializerBuilder
{
    /// <summary>
    ///     Build a serializer with a Camel case policy.
    /// </summary>
    /// <returns>A serializer.</returns>
    public static JsonSerializer BuildWithCamelCase() => BuildSerializer(JsonNamingPolicy.CamelCase);
    
    /// <summary>
    ///     Build a serializer with a Snake case policy.
    /// </summary>
    /// <returns>A serializer.</returns>
    public static JsonSerializer BuildWithSnakeCase() => BuildSerializer(JsonNamingPolicy.SnakeCaseLower);
    
    private static JsonSerializer BuildSerializer(JsonNamingPolicy policy) => new JsonSerializer(policy)
        .WithConverter(new MaybeJsonConverter<string>())
        .WithConverter(new EnumDescriptionJsonConverter<MessagesChannel>())
        .WithConverter(new EnumDescriptionJsonConverter<MessagesMessageType>())
        .WithConverter(new EnumDescriptionJsonConverter<FraudScoreLabel>())
        .WithConverter(new EnumDescriptionJsonConverter<LayoutType>())
        .WithConverter(new EnumDescriptionJsonConverter<RenderResolution>())
        .WithConverter(new EnumDescriptionJsonConverter<OutputMode>())
        .WithConverter(new EnumDescriptionJsonConverter<StreamMode>())
        .WithConverter(new EnumDescriptionJsonConverter<Broadcast.BroadcastStatus>())
        .WithConverter(new EnumDescriptionJsonConverter<Broadcast.RtmpStatus>())
        .WithConverter(new HttpMethodJsonConverter())
        .WithConverter(new PhoneNumberJsonConverter())
        .WithConverter(new EmailJsonConverter())
        .WithConverter(new LocaleJsonConverter())
        .WithConverter(new EnumDescriptionJsonConverter<ChannelType>());
}