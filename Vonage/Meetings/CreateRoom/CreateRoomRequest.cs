using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Meetings.Common;

namespace Vonage.Meetings.CreateRoom;

public readonly struct CreateRoomRequest : IVonageRequest
{
    public Room.Features AvailableFeatures { get; }
    public Room.Callback CallbackUrls { get; }

    public string DisplayName { get; }
    public bool ExpiresAfterUse { get; }
    public string ExpiresAt { get; }
    public Room.JoinOptions InitialJoinOptions { get; }
    public RoomApprovalLevel JoinApprovalLevel { get; }
    public string Metadata { get; }
    public Room.RecordingOptions RecordingOptions { get; }
    public string ThemeId { get; }
    public RoomType Type { get; }

    internal CreateRoomRequest(string displayName, string metadata, RoomType type, string expiresAt,
        bool expiresAfterUse, string themeId, RoomApprovalLevel joinApprovalLevel,
        Room.RecordingOptions recordingOptions, Room.JoinOptions initialJoinOptions, Room.Callback callbackUrls,
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

    public HttpRequestMessage BuildRequestMessage(string token) => throw new NotImplementedException();

    public string GetEndpointPath() => throw new NotImplementedException();

    public static Result<CreateRoomRequest> Parse(
        string displayName,
        string metadata,
        RoomType type,
        string expiresAt,
        bool expiresAfterUse,
        string themeId,
        RoomApprovalLevel joinApprovalLevel,
        Room.RecordingOptions recordingOptions,
        Room.JoinOptions initialJoinOptions,
        Room.Callback callbackUrls,
        Room.Features availableFeatures)
    {
        throw new NotImplementedException();
    }
}