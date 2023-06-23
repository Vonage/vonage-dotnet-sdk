using System;
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
    public Room.Features AvailableFeatures { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(VonageMaybeJsonConverter<Room.Callback>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<Room.Callback> CallbackUrls { get; internal init; }

    /// <summary>
    /// Close the room after a session ends. Only relevant for long_term rooms.
    /// </summary>
    public bool ExpireAfterUse { get; internal init; }

    /// <summary>
    /// The time for when the room will be expired, expressed in ISO 8601 format.
    /// </summary>
    [JsonConverter(typeof(VonageMaybeJsonConverter<DateTime>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<DateTime> ExpiresAt { get; internal init; }

    /// <summary>
    /// </summary>
    public Room.JoinOptions InitialJoinOptions { get; internal init; }

    /// <summary>
    /// The level of approval needed to join the meeting in the room. When set to "after_owner_only" the participants will join the meeting only after the host joined. When set to "explicit_approval" the participants will join the waiting room and the host will deny/approve them.
    /// </summary>
    [JsonConverter(typeof(EnumDescriptionJsonConverter<RoomApprovalLevel>))]
    public RoomApprovalLevel JoinApprovalLevel { get; internal init; }

    /// <summary>
    /// The room id.
    /// </summary>
    [JsonIgnore]
    public Guid RoomId { get; internal init; }

    /// <summary>
    /// The theme id for the room.
    /// </summary>
    [JsonConverter(typeof(VonageMaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> ThemeId { get; internal init; }

    /// <summary>
    ///     Initializes a builder for UpdateRoomRequest.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForRoomId Build() => new UpdateRoomRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(new HttpMethod("PATCH"), this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/meetings/rooms/{this.RoomId}";

    private StringContent GetRequestContent() =>
        new(JsonSerializer.BuildWithSnakeCase().SerializeObject(new {UpdateDetails = this}),
            Encoding.UTF8,
            "application/json");
}