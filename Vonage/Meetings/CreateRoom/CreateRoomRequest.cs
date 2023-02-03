using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Client;
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
    public Room.Callback? CallbackUrls { get; }

    /// <summary>
    /// </summary>
    public string DisplayName { get; }

    /// <summary>
    /// </summary>
    public bool ExpireAfterUse { get; }

    /// <summary>
    /// </summary>
    public string ExpiresAt { get; }

    /// <summary>
    /// </summary>
    public Room.JoinOptions InitialJoinOptions { get; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(EnumDescriptionJsonConverter<RoomApprovalLevel>))]
    public RoomApprovalLevel JoinApprovalLevel { get; }

    /// <summary>
    /// </summary>
    public string Metadata { get; }

    /// <summary>
    /// </summary>
    public Room.RecordingOptions? RecordingOptions { get; }

    /// <summary>
    /// </summary>
    public string ThemeId { get; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(EnumDescriptionJsonConverter<RoomType>))]
    public RoomType? Type { get; }

    internal CreateRoomRequest(string displayName, string metadata, RoomType? type, string expiresAt,
        bool expireAfterUse, string themeId, RoomApprovalLevel joinApprovalLevel,
        Room.RecordingOptions? recordingOptions, Room.JoinOptions initialJoinOptions, Room.Callback? callbackUrls,
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