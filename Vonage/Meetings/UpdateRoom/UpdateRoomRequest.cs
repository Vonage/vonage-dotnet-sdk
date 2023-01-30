using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
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
    public Room.Callback? CallbackUrls { get; }

    /// <summary>
    /// </summary>
    public bool ExpiresAfterUse { get; }

    /// <summary>
    /// </summary>
    public string ExpiresAt { get; }

    /// <summary>
    /// </summary>
    public Room.JoinOptions InitialJoinOptions { get; }

    /// <summary>
    /// </summary>
    public RoomApprovalLevel JoinApprovalLevel { get; }

    /// <summary>
    /// </summary>
    public string RoomId { get; }

    /// <summary>
    /// </summary>
    public string ThemeId { get; }

    internal UpdateRoomRequest(string roomId, string expiresAt,
        bool expiresAfterUse, string themeId, RoomApprovalLevel joinApprovalLevel,
        Room.JoinOptions initialJoinOptions, Room.Callback? callbackUrls,
        Room.Features availableFeatures)
    {
        this.RoomId = roomId;
        this.ExpiresAt = expiresAt;
        this.ExpiresAfterUse = expiresAfterUse;
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
        new(JsonSerializerBuilder.Build().SerializeObject(this),
            Encoding.UTF8,
            "application/json");
}