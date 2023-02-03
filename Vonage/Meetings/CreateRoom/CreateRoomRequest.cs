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
    public Room.Features AvailableFeatures { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(VonageMaybeJsonConverter<Room.Callback>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<Room.Callback> CallbackUrls { get; internal init; }

    /// <summary>
    /// </summary>
    public string DisplayName { get; internal init; }

    /// <summary>
    /// Close the room after a session ends. Only relevant for long_term rooms.
    /// </summary>
    public bool ExpireAfterUse { get; internal init; }

    /// <summary>
    /// The time for when the room will be expired, expressed in ISO 8601 format. Required only for long-term room creation.
    /// </summary>
    [JsonConverter(typeof(VonageMaybeJsonConverter<string>))]
    public Maybe<string> ExpiresAt { get; internal init; }

    /// <summary>
    /// </summary>
    public Room.JoinOptions InitialJoinOptions { get; internal init; }

    /// <summary>
    /// The level of approval needed to join the meeting in the room. When set to "after_owner_only" the participants will join the meeting only after the host joined. When set to "explicit_approval" the participants will join the waiting room and the host will deny/approve them.
    /// </summary>
    [JsonConverter(typeof(EnumDescriptionJsonConverter<RoomApprovalLevel>))]
    public RoomApprovalLevel JoinApprovalLevel { get; internal init; }

    /// <summary>
    /// Free text that can be attached to a room. This will be passed in the form of a header in events related to this room.
    /// </summary>
    [JsonConverter(typeof(VonageMaybeJsonConverter<string>))]
    public Maybe<string> Metadata { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(VonageMaybeJsonConverter<Room.RecordingOptions>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<Room.RecordingOptions> RecordingOptions { get; internal init; }

    /// <summary>
    /// The theme id for the room
    /// </summary>
    [JsonConverter(typeof(VonageMaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> ThemeId { get; internal init; }

    /// <summary>
    /// Represents the type of the room.
    /// </summary>
    [JsonConverter(typeof(VonageMaybeJsonConverter<RoomType>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<RoomType> Type { get; internal init; }

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