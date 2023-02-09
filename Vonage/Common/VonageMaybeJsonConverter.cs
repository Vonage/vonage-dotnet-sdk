using Vonage.Common.Serialization;
using Vonage.Meetings.Common;

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
    public VonageMaybeJsonConverter() =>
        this.serializer = JsonSerializer.BuildWithSnakeCase()
            .WithConverter(new EnumDescriptionJsonConverter<RoomApprovalLevel>())
            .WithConverter(new EnumDescriptionJsonConverter<RecordingStatus>())
            .WithConverter(new EnumDescriptionJsonConverter<RoomType>())
            .WithConverter(new EnumDescriptionJsonConverter<RoomMicrophoneState>())
            .WithConverter(new EnumDescriptionJsonConverter<ThemeDomain>())
            .WithConverter(new EnumDescriptionJsonConverter<ThemeLogoType>());
}