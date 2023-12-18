using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Meetings.Common;
using Vonage.Serialization;

namespace Vonage.Meetings.CreateRoom;

/// <summary>
///     Represents a request to create a room.
/// </summary>
public readonly struct CreateRoomRequest : IVonageRequest
{
    /// <summary>
    /// </summary>
    public Room.Features AvailableFeatures { get; internal init; }

    /// <summary>
    /// </summary>
    public Maybe<Room.Callback> CallbackUrls { get; internal init; }

    /// <summary>
    /// </summary>
    public string DisplayName { get; internal init; }

    /// <summary>
    /// Close the room after a session ends. Only relevant for long_term rooms.
    /// </summary>
    public bool ExpireAfterUse { get; internal init; }

    /// <summary>
    /// The time for when the room will be expired. Required only for long-term room creation.
    /// </summary>
    public Maybe<DateTime> ExpiresAt { get; internal init; }

    /// <summary>
    /// </summary>
    public Room.JoinOptions InitialJoinOptions { get; internal init; }

    /// <summary>
    /// The level of approval needed to join the meeting in the room. When set to "after_owner_only" the participants will join the meeting only after the host joined. When set to "explicit_approval" the participants will join the waiting room and the host will deny/approve them.
    /// </summary>
    public Maybe<RoomApprovalLevel> JoinApprovalLevel { get; internal init; }

    /// <summary>
    /// Free text that can be attached to a room. This will be passed in the form of a header in events related to this room.
    /// </summary>
    public Maybe<string> Metadata { get; internal init; }

    /// <summary>
    /// </summary>
    public Maybe<Room.RecordingOptions> RecordingOptions { get; internal init; }

    /// <summary>
    /// The theme id for the room
    /// </summary>
    public Maybe<string> ThemeId { get; internal init; }

    /// <summary>
    /// Represents the type of the room.
    /// </summary>
    public RoomType Type { get; internal init; }

    /// <summary>
    ///     Provides options to customize the user interface.
    /// </summary>
    public UiSettings UserInterfaceSettings { get; internal init; }

    /// <summary>
    ///     Initializes a builder for CreateRoomRequest.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForDisplayName Build() => new CreateRoomRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => "/v1/meetings/rooms";

    private StringContent GetRequestContent()
    {
        var values = new Dictionary<string, object>();
        values.Add("available_features", this.AvailableFeatures);
        this.CallbackUrls.IfSome(value => values.Add("callback_urls", value));
        values.Add("display_name", this.DisplayName);
        if (this.HasLongTermType())
        {
            values.Add("expire_after_use", this.ExpireAfterUse);
            this.ExpiresAt.IfSome(value => values.Add("expires_at", value.ToString("yyyy-MM-dd HH:mm:ss")));
        }

        values.Add("initial_join_options", this.InitialJoinOptions);
        this.JoinApprovalLevel.IfSome(value => values.Add("join_approval_level", value));
        this.Metadata.IfSome(value => values.Add("metadata", value));
        this.RecordingOptions.IfSome(value => values.Add("recording_options", value));
        this.ThemeId.IfSome(value => values.Add("theme_id", value));
        values.Add("type", this.Type);
        values.Add("ui_settings", this.UserInterfaceSettings);
        return new StringContent(JsonSerializerBuilder
                .BuildWithSnakeCase()
                .WithConverter(new EnumDescriptionJsonConverter<RoomApprovalLevel>())
                .WithConverter(new EnumDescriptionJsonConverter<RecordingStatus>())
                .WithConverter(new EnumDescriptionJsonConverter<RoomType>())
                .WithConverter(new EnumDescriptionJsonConverter<RoomMicrophoneState>())
                .WithConverter(new EnumDescriptionJsonConverter<ThemeDomain>())
                .WithConverter(new EnumDescriptionJsonConverter<ThemeLogoType>())
                .SerializeObject(values),
            Encoding.UTF8,
            "application/json");
    }

    private bool HasLongTermType() => this.Type == RoomType.LongTerm;
}