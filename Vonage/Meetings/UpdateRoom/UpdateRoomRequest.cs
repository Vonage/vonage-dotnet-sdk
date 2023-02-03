using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Meetings.Common;

namespace Vonage.Meetings.UpdateRoom;

/// <summary>
///     Represents a request to update a room.
/// </summary>
public readonly struct UpdateRoomRequest : IVonageRequest
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
    /// Close the room after a session ends. Only relevant for long_term rooms.
    /// </summary>
    public bool ExpireAfterUse { get; }

    /// <summary>
    /// The time for when the room will be expired, expressed in ISO 8601 format.
    /// </summary>
    [JsonConverter(typeof(VonageMaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
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
    /// The room id.
    /// </summary>
    [JsonIgnore]
    public string RoomId { get; }

    /// <summary>
    /// The theme id for the room.
    /// </summary>
    [JsonConverter(typeof(VonageMaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> ThemeId { get; }

    internal UpdateRoomRequest(string roomId, Maybe<string> expiresAt,
        bool expireAfterUse, Maybe<string> themeId, RoomApprovalLevel joinApprovalLevel,
        Room.JoinOptions initialJoinOptions, Maybe<Room.Callback> callbackUrls,
        Room.Features availableFeatures)
    {
        this.RoomId = roomId;
        this.ExpiresAt = expiresAt;
        this.ExpireAfterUse = expireAfterUse;
        this.ThemeId = themeId;
        this.JoinApprovalLevel = joinApprovalLevel;
        this.InitialJoinOptions = initialJoinOptions;
        this.CallbackUrls = callbackUrls;
        this.AvailableFeatures = availableFeatures;
    }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(new HttpMethod("PATCH"), this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/beta/meetings/rooms/{this.RoomId}";

    private StringContent GetRequestContent() =>
        new(JsonSerializer.BuildWithSnakeCase().SerializeObject(new {UpdateDetails = this}),
            Encoding.UTF8,
            "application/json");
}