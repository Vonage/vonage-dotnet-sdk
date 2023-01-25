using System;
using System.Net.Http;
using Vonage.Common.Client;
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
    public string Metadata { get; }

    /// <summary>
    /// </summary>
    public Room.RecordingOptions? RecordingOptions { get; }

    /// <summary>
    /// </summary>
    public string ThemeId { get; }

    /// <summary>
    /// </summary>
    public RoomType? Type { get; }

    internal CreateRoomRequest(string displayName, string metadata, RoomType? type, string expiresAt,
        bool expiresAfterUse, string themeId, RoomApprovalLevel joinApprovalLevel,
        Room.RecordingOptions? recordingOptions, Room.JoinOptions initialJoinOptions, Room.Callback? callbackUrls,
        Room.Features availableFeatures)
    {
        this.DisplayName = displayName;
        this.Metadata = metadata;
        this.Type = type;
        this.ExpiresAt = expiresAt;
        this.ExpiresAfterUse = expiresAfterUse;
        this.ThemeId = themeId;
        this.JoinApprovalLevel = joinApprovalLevel;
        this.RecordingOptions = recordingOptions;
        this.InitialJoinOptions = initialJoinOptions;
        this.CallbackUrls = callbackUrls;
        this.AvailableFeatures = availableFeatures;
    }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage(string token) => throw new NotImplementedException();

    /// <inheritdoc />
    public string GetEndpointPath() => throw new NotImplementedException();
}