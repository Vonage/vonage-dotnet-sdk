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
    /// </summary>
    public bool ExpireAfterUse { get; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(VonageMaybeJsonConverter<string>))]
    public Maybe<string> ExpiresAt { get; }

    /// <summary>
    /// </summary>
    public Room.JoinOptions InitialJoinOptions { get; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(EnumDescriptionJsonConverter<RoomApprovalLevel>))]
    public RoomApprovalLevel JoinApprovalLevel { get; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(VonageMaybeJsonConverter<string>))]
    public Maybe<string> Metadata { get; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(VonageMaybeJsonConverter<Room.RecordingOptions>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<Room.RecordingOptions> RecordingOptions { get; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(VonageMaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> ThemeId { get; }

    /// <summary>
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