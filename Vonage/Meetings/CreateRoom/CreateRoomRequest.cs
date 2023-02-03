using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Meetings.Common;

namespace Vonage.Meetings.CreateRoom;

/// <summary>
///     Represents a request to create a room.
/// </summary>
public readonly struct CreateRoomRequest : IVonageRequest
{
    /// <summary>
    /// </summary>
    public Room.Features AvailableFeatures { get; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(VonageMaybeJsonConverter<Room.Callback>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<Room.Callback> CallbackUrls { get; }

    /// <summary>
    /// </summary>
    public string DisplayName { get; }

    /// <summary>
    /// Close the room after a session ends. Only relevant for long_term rooms.
    /// </summary>
    public bool ExpireAfterUse { get; }

    /// <summary>
    /// The time for when the room will be expired, expressed in ISO 8601 format. Required only for long-term room creation.
    /// </summary>
    [JsonConverter(typeof(VonageMaybeJsonConverter<string>))]
    public Maybe<string> ExpiresAt { get; }

    /// <summary>
    /// </summary>
    public Room.JoinOptions InitialJoinOptions { get; }

    /// <summary>
    /// The level of approval needed to join the meeting in the room. When set to "after_owner_only" the participants will join the meeting only after the host joined. When set to "explicit_approval" the participants will join the waiting room and the host will deny/approve them.
    /// </summary>
    [JsonConverter(typeof(EnumDescriptionJsonConverter<RoomApprovalLevel>))]
    public RoomApprovalLevel JoinApprovalLevel { get; }

    /// <summary>
    /// Free text that can be attached to a room. This will be passed in the form of a header in events related to this room.
    /// </summary>
    [JsonConverter(typeof(VonageMaybeJsonConverter<string>))]
    public Maybe<string> Metadata { get; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(VonageMaybeJsonConverter<Room.RecordingOptions>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<Room.RecordingOptions> RecordingOptions { get; }

    /// <summary>
    /// The theme id for the room
    /// </summary>
    [JsonConverter(typeof(VonageMaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> ThemeId { get; }

    /// <summary>
    /// Represents the type of the room.
    /// </summary>
    [JsonConverter(typeof(VonageMaybeJsonConverter<RoomType>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<RoomType> Type { get; }

    internal CreateRoomRequest(string displayName, Maybe<string> metadata, Maybe<RoomType> type,
        Maybe<string> expiresAt,
        bool expireAfterUse, Maybe<string> themeId, RoomApprovalLevel joinApprovalLevel,
        Maybe<Room.RecordingOptions> recordingOptions, Room.JoinOptions initialJoinOptions,
        Maybe<Room.Callback> callbackUrls,
        Room.Features availableFeatures)
    {
        this.DisplayName = displayName;
        this.Metadata = metadata;
        this.Type = type;
        this.ExpiresAt = expiresAt;
        this.ExpireAfterUse = expireAfterUse;
        this.ThemeId = themeId;
        this.JoinApprovalLevel = joinApprovalLevel;
        this.RecordingOptions = recordingOptions;
        this.InitialJoinOptions = initialJoinOptions;
        this.CallbackUrls = callbackUrls;
        this.AvailableFeatures = availableFeatures;
    }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => "/beta/meetings/rooms";

    private StringContent GetRequestContent() =>
        new(JsonSerializer.BuildWithSnakeCase().SerializeObject(this),
            Encoding.UTF8,
            "application/json");
}