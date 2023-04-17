using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Meetings.Common;

namespace Vonage.Meetings.CreateRoom;

/// <inheritdoc />
internal class CreateRoomRequestBuilder : ICreateRoomRequestBuilder
{
    private const int DisplayNameMaxLength = 200;
    private const int MetadataMaxLength = 500;
    private bool expireAfterUse;

    private Room.Features features = new()
        {IsChatAvailable = true, IsRecordingAvailable = true, IsWhiteboardAvailable = true};

    private Room.JoinOptions joinOptions = new() {MicrophoneState = RoomMicrophoneState.Default};
    private Maybe<Room.Callback> callback;
    private Maybe<DateTime> expiresAt;
    private Maybe<Room.RecordingOptions> recordingOptions;
    private Maybe<RoomApprovalLevel> approvalLevel;
    private Maybe<string> metadata;
    private Maybe<string> themeId;
    private RoomType roomType = RoomType.Instant;
    private readonly string displayName;

    internal CreateRoomRequestBuilder(string displayName) => this.displayName = displayName;

    /// <inheritdoc />
    public ICreateRoomRequestBuilder AsLongTermRoom(DateTime expiration)
    {
        this.roomType = RoomType.LongTerm;
        this.expiresAt = expiration;
        return this;
    }

    /// <inheritdoc />
    public Result<CreateRoomRequest> Create() =>
        Result<CreateRoomRequest>
            .FromSuccess(new CreateRoomRequest
            {
                DisplayName = this.displayName,
                Metadata = this.metadata,
                Type = this.roomType,
                ExpiresAt = this.expiresAt,
                ExpireAfterUse = this.expireAfterUse,
                ThemeId = this.themeId,
                JoinApprovalLevel = this.approvalLevel,
                RecordingOptions = this.recordingOptions,
                InitialJoinOptions = this.joinOptions,
                CallbackUrls = this.callback,
                AvailableFeatures = this.features,
            })
            .Bind(VerifyDisplayName)
            .Bind(VerifyDisplayNameLength)
            .Bind(VerifyMetadataLength);

    /// <inheritdoc />
    public ICreateRoomRequestBuilder ExpireAfterUse()
    {
        this.expireAfterUse = true;
        return this;
    }

    /// <inheritdoc />
    public ICreateRoomRequestBuilder WithApprovalLevel(RoomApprovalLevel level)
    {
        this.approvalLevel = level;
        return this;
    }

    /// <inheritdoc />
    public ICreateRoomRequestBuilder WithCallback(Room.Callback callbackUrls)
    {
        this.callback = callbackUrls;
        return this;
    }

    /// <inheritdoc />
    public ICreateRoomRequestBuilder WithFeatures(Room.Features availableFeatures)
    {
        this.features = availableFeatures;
        return this;
    }

    /// <inheritdoc />
    public ICreateRoomRequestBuilder WithInitialJoinOptions(Room.JoinOptions options)
    {
        this.joinOptions = options;
        return this;
    }

    /// <inheritdoc />
    public ICreateRoomRequestBuilder WithMetadata(string data)
    {
        this.metadata = data;
        return this;
    }

    /// <inheritdoc />
    public ICreateRoomRequestBuilder WithRecordingOptions(Room.RecordingOptions options)
    {
        this.recordingOptions = options;
        return this;
    }

    /// <inheritdoc />
    public ICreateRoomRequestBuilder WithThemeId(string theme)
    {
        this.themeId = theme;
        return this;
    }

    private static Result<CreateRoomRequest> VerifyDisplayName(CreateRoomRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.DisplayName, nameof(request.DisplayName));

    private static Result<CreateRoomRequest> VerifyDisplayNameLength(CreateRoomRequest request) =>
        InputValidation
            .VerifyLowerOrEqualThan(request, request.DisplayName.Length, DisplayNameMaxLength,
                nameof(request.DisplayName));

    private static Result<CreateRoomRequest> VerifyMetadataLength(CreateRoomRequest request) =>
        request
            .Metadata
            .Match(
                some => InputValidation.VerifyLowerOrEqualThan(request, some.Length, MetadataMaxLength,
                    nameof(request.Metadata)),
                () => request);
}

/// <summary>
///     Represents a builder for CreateRoomRequest.
/// </summary>
public interface ICreateRoomRequestBuilder : IVonageRequestBuilder<CreateRoomRequest>
{
    /// <summary>
    ///     Sets the room as long-term.
    /// </summary>
    /// <param name="expiration">The expiration date.</param>
    /// <returns>The builder.</returns>
    ICreateRoomRequestBuilder AsLongTermRoom(DateTime expiration);

    /// <summary>
    ///     Sets the room to expire after use.
    /// </summary>
    /// <returns>The builder.</returns>
    ICreateRoomRequestBuilder ExpireAfterUse();

    /// <summary>
    ///     Sets the approval level on the builder.
    /// </summary>
    /// <param name="level">The approval level.</param>
    /// <returns>The builder.</returns>
    ICreateRoomRequestBuilder WithApprovalLevel(RoomApprovalLevel level);

    /// <summary>
    ///     Sets the callback urls on the builder.
    /// </summary>
    /// <param name="callbackUrls">The callback urls.</param>
    /// <returns>The builder.</returns>
    ICreateRoomRequestBuilder WithCallback(Room.Callback callbackUrls);

    /// <summary>
    ///     Sets the available features on the builder.
    /// </summary>
    /// <param name="availableFeatures">The available features.</param>
    /// <returns>The builder.</returns>
    ICreateRoomRequestBuilder WithFeatures(Room.Features availableFeatures);

    /// <summary>
    ///     Sets the join options on the builder.
    /// </summary>
    /// <param name="options">The join options.</param>
    /// <returns>The builder.</returns>
    ICreateRoomRequestBuilder WithInitialJoinOptions(Room.JoinOptions options);

    /// <summary>
    ///     Sets the medata on the builder.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns>The builder.</returns>
    ICreateRoomRequestBuilder WithMetadata(string data);

    /// <summary>
    ///     Sets the recording options on the builder.
    /// </summary>
    /// <param name="options">The recording options.</param>
    /// <returns>The builder.</returns>
    ICreateRoomRequestBuilder WithRecordingOptions(Room.RecordingOptions options);

    /// <summary>
    ///     Sets the theme  identifier on the builder.
    /// </summary>
    /// <param name="theme">The theme identifier.</param>
    /// <returns>The builder.</returns>
    ICreateRoomRequestBuilder WithThemeId(string theme);
}