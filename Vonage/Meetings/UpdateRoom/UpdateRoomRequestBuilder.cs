using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Meetings.Common;

namespace Vonage.Meetings.UpdateRoom;

/// <summary>
///     Represents a builder for UpdateRoomRequest.
/// </summary>
internal class UpdateRoomRequestBuilder : IBuilderForRoomId, IBuilderForOptional
{
    private bool expireAfterUse;

    private Room.Features features = new()
        {IsChatAvailable = true, IsRecordingAvailable = true, IsWhiteboardAvailable = true};

    private Guid roomId;
    private Room.JoinOptions joinOptions = new() {MicrophoneState = RoomMicrophoneState.Default};
    private Maybe<Room.Callback> callback;
    private Maybe<DateTime> expiresAt;
    private Maybe<string> themeId;
    private RoomApprovalLevel approvalLevel = RoomApprovalLevel.None;

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

    /// <inheritdoc />
    public IBuilderForOptional ExpireAfterUse()
    {
        this.expireAfterUse = true;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithApprovalLevel(RoomApprovalLevel level)
    {
        this.approvalLevel = level;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithCallback(Room.Callback callbackUrls)
    {
        this.callback = callbackUrls;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithExpiresAt(DateTime expiration)
    {
        this.expiresAt = expiration;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithFeatures(Room.Features availableFeatures)
    {
        this.features = availableFeatures;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithInitialJoinOptions(Room.JoinOptions options)
    {
        this.joinOptions = options;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithRoomId(Guid value)
    {
        this.roomId = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithThemeId(string theme)
    {
        this.themeId = theme;
        return this;
    }

    private static Result<UpdateRoomRequest> VerifyRoomId(UpdateRoomRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.RoomId, nameof(request.RoomId));
}

/// <summary>
///     Represents a builder for RoomId.
/// </summary>
public interface IBuilderForRoomId
{
    /// <summary>
    ///     Sets the RoomId.
    /// </summary>
    /// <param name="value">The room Id.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithRoomId(Guid value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<UpdateRoomRequest>
{
    /// <summary>
    ///     Sets the room to expire after use.
    /// </summary>
    /// <returns>The builder.</returns>
    IBuilderForOptional ExpireAfterUse();

    /// <summary>
    ///     Sets the approval level on the builder.
    /// </summary>
    /// <param name="level">The approval level.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithApprovalLevel(RoomApprovalLevel level);

    /// <summary>
    ///     Sets the callback urls on the builder.
    /// </summary>
    /// <param name="callbackUrls">The callback urls.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithCallback(Room.Callback callbackUrls);

    /// <summary>
    /// </summary>
    /// <param name="expiration"></param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithExpiresAt(DateTime expiration);

    /// <summary>
    ///     Sets the available features on the builder.
    /// </summary>
    /// <param name="availableFeatures">The available features.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithFeatures(Room.Features availableFeatures);

    /// <summary>
    ///     Sets the join options on the builder.
    /// </summary>
    /// <param name="options">The join options.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithInitialJoinOptions(Room.JoinOptions options);

    /// <summary>
    ///     Sets the theme  identifier on the builder.
    /// </summary>
    /// <param name="theme">The theme identifier.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithThemeId(string theme);
}