using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Meetings.Common;

namespace Vonage.Meetings.UpdateRoom;

/// <summary>
///     Represents a builder for UpdateRoomRequest.
/// </summary>
public class UpdateRoomRequestBuilder
{
    private bool expireAfterUse;

    private Room.Features features = new()
        {IsChatAvailable = true, IsRecordingAvailable = true, IsWhiteboardAvailable = true};

    private Room.JoinOptions joinOptions = new() {MicrophoneState = RoomMicrophoneState.Default};
    private Maybe<Room.Callback> callback;
    private Maybe<string> expiresAt;
    private Maybe<string> themeId;
    private RoomApprovalLevel approvalLevel = RoomApprovalLevel.None;
    private readonly string roomId;

    private UpdateRoomRequestBuilder(string roomId) => this.roomId = roomId;

    /// <summary>
    ///     Initializes a builder for UpdateRoomRequest.
    /// </summary>
    /// <param name="roomId">The room id.</param>
    /// <returns>The builder.</returns>
    public static UpdateRoomRequestBuilder Build(string roomId) => new(roomId);

    /// <summary>
    ///     Creates the request.
    /// </summary>
    /// <returns>The request if validation succeeded, a failure if it failed.</returns>
    public Result<UpdateRoomRequest> Create() =>
        Result<UpdateRoomRequest>
            .FromSuccess(new UpdateRoomRequest
            {
                ThemeId = this.themeId,
                AvailableFeatures = this.features,
                ExpireAfterUse = this.expireAfterUse,
                RoomId = this.roomId,
                CallbackUrls = this.callback,
                ExpiresAt = this.expiresAt,
                InitialJoinOptions = this.joinOptions,
                JoinApprovalLevel = this.approvalLevel,
            })
            .Bind(VerifyRoomId);

    /// <summary>
    ///     Sets the room to expire after use.
    /// </summary>
    /// <returns>The builder.</returns>
    public UpdateRoomRequestBuilder ExpireAfterUse()
    {
        this.expireAfterUse = true;
        return this;
    }

    /// <summary>
    ///     Sets the approval level on the builder.
    /// </summary>
    /// <param name="level">The approval level.</param>
    /// <returns>The builder.</returns>
    public UpdateRoomRequestBuilder WithApprovalLevel(RoomApprovalLevel level)
    {
        this.approvalLevel = level;
        return this;
    }

    /// <summary>
    ///     Sets the callback urls on the builder.
    /// </summary>
    /// <param name="callbackUrls">The callback urls.</param>
    /// <returns>The builder.</returns>
    public UpdateRoomRequestBuilder WithCallback(Room.Callback callbackUrls)
    {
        this.callback = callbackUrls;
        return this;
    }

    /// <summary>
    /// </summary>
    /// <param name="expiration"></param>
    /// <returns>The builder.</returns>
    public UpdateRoomRequestBuilder WithExpiresAt(string expiration)
    {
        this.expiresAt = expiration;
        return this;
    }

    /// <summary>
    ///     Sets the available features on the builder.
    /// </summary>
    /// <param name="availableFeatures">The available features.</param>
    /// <returns>The builder.</returns>
    public UpdateRoomRequestBuilder WithFeatures(Room.Features availableFeatures)
    {
        this.features = availableFeatures;
        return this;
    }

    /// <summary>
    ///     Sets the join options on the builder.
    /// </summary>
    /// <param name="options">The join options.</param>
    /// <returns>The builder.</returns>
    public UpdateRoomRequestBuilder WithInitialJoinOptions(Room.JoinOptions options)
    {
        this.joinOptions = options;
        return this;
    }

    /// <summary>
    ///     Sets the theme  identifier on the builder.
    /// </summary>
    /// <param name="theme">The theme identifier.</param>
    /// <returns>The builder.</returns>
    public UpdateRoomRequestBuilder WithThemeId(string theme)
    {
        this.themeId = theme;
        return this;
    }

    private static Result<UpdateRoomRequest> VerifyRoomId(UpdateRoomRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.RoomId, nameof(request.RoomId));
}