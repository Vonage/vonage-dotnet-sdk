using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Meetings.Common;

namespace Vonage.Meetings.CreateRoom;

/// <summary>
/// </summary>
public class CreateRoomRequestBuilder
{
    private const int DisplayNameMaxLength = 200;
    private readonly Room.Features availableFeatures = new(true, true, true);
    private readonly Room.JoinOptions initialJoinOptions = new(RoomMicrophoneState.Default);

    private readonly string displayName;
    private string metadata;

    private CreateRoomRequestBuilder(string displayName) => this.displayName = displayName;

    /// <summary>
    /// </summary>
    /// <param name="displayName"></param>
    /// <returns></returns>
    public static CreateRoomRequestBuilder Build(string displayName) => new(displayName);

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public Result<CreateRoomRequest> Create() =>
        Result<CreateRoomRequest>
            .FromSuccess(new CreateRoomRequest(
                this.displayName,
                this.metadata,
                RoomType.Instant,
                "",
                false,
                "",
                RoomApprovalLevel.None,
                new Room.RecordingOptions(),
                this.initialJoinOptions,
                new Room.Callback(),
                this.availableFeatures))
            .Bind(VerifyDisplayName)
            .Bind(VerifyDisplayNameLength)
            .Bind(VerifyMetadataLength);

    public CreateRoomRequestBuilder WithMetadata(string empty)
    {
        this.metadata = empty;
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
        request.Metadata is null
            ? request
            : InputValidation
                .VerifyLowerOrEqualThan(request, request.Metadata.Length, 500,
                    nameof(request.Metadata));
}