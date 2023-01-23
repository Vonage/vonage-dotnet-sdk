using System.Collections.Generic;
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Serialization;
using Vonage.Meetings.GetApplicationThemes;
using Yoh.Text.Json.NamingPolicies;

namespace Vonage.Meetings.Common;

/// <summary>
///     Builder for Meetings serializer.
/// </summary>
public static class JsonSerializerBuilder
{
    /// <summary>
    ///     Build a serializer.
    /// </summary>
    /// <returns>A serializer.</returns>
    public static JsonSerializer Build() => new(new List<JsonConverter>
        {
            new EnumDescriptionJsonConverter<RoomType>(),
            new EnumDescriptionJsonConverter<RoomApprovalLevel>(),
            new EnumDescriptionJsonConverter<RoomMicrophoneState>(),
            new EnumDescriptionJsonConverter<RecordingStatus>(),
            new EnumDescriptionJsonConverter<ThemeDomain>(),
        },
        JsonNamingPolicies.SnakeCaseLower);
}