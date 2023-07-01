using System;
using System.Linq;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Meetings.Common;

namespace Vonage.Meetings.UpdateRoom;

internal class UpdateRoomRequestBuilder : IBuilderForRoomId, IBuilderForOptional
{
    private Guid roomId;
    private Maybe<bool> expireAfterUse = Maybe<bool>.None;
    private Maybe<Room.Callback> callback;
    private Maybe<DateTime> expiresAt;
    private Maybe<Room.Features> features = Maybe<Room.Features>.None;
    private Maybe<Room.JoinOptions> joinOptions = Maybe<Room.JoinOptions>.None;
    private Maybe<RoomApprovalLevel> approvalLevel = Maybe<RoomApprovalLevel>.None;
    private Maybe<string> themeId;
    private Maybe<UiSettings> uiSettings = Maybe<UiSettings>.None;

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
                UserInterfaceSettings = this.uiSettings,
            })
            .Map(InputEvaluation<UpdateRoomRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyAtLeastOneValue, VerifyRoomId));

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

    /// <inheritdoc />
    public IBuilderForOptional WithUserInterfaceSettings(UiSettings value)
    {
        this.uiSettings = value;
        return this;
    }

    private static Result<UpdateRoomRequest> VerifyAtLeastOneValue(UpdateRoomRequest request) =>
        new[]
        {
            request.AvailableFeatures.IsSome,
            request.CallbackUrls.IsSome,
            request.ExpireAfterUse.IsSome,
            request.ExpiresAt.IsSome,
            request.ThemeId.IsSome,
            request.InitialJoinOptions.IsSome,
            request.JoinApprovalLevel.IsSome,
            request.UserInterfaceSettings.IsSome,
        }.Any(_ => _)
            ? request
            : Result<UpdateRoomRequest>.FromFailure(
                ResultFailure.FromErrorMessage("At least one property must be updated."));

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

    /// <summary>
    ///     Sets the options to customize the user interface.
    /// </summary>
    /// <param name="value">The options to customize the user interface.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithUserInterfaceSettings(UiSettings value);
}