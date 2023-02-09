using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Server.Video.Archives.AddStream;

/// <inheritdoc />
public class AddStreamRequestBuilder : IVonageRequestBuilder<AddStreamRequest>
{
    private bool hasAudio = true;
    private bool hasVideo = true;
    private readonly Guid applicationId;
    private readonly Guid archiveId;
    private readonly Guid streamId;

    private AddStreamRequestBuilder(Guid applicationId, Guid archiveId, Guid streamId)
    {
        this.applicationId = applicationId;
        this.archiveId = archiveId;
        this.streamId = streamId;
    }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <param name="applicationId">The application id.</param>
    /// <param name="archiveId">The archive id.</param>
    /// <param name="streamId">The stream id.</param>
    /// <returns>The builder.</returns>
    public static AddStreamRequestBuilder Build(Guid applicationId, Guid archiveId, Guid streamId) =>
        new(applicationId, archiveId, streamId);

    /// <inheritdoc />
    public Result<AddStreamRequest> Create() => Result<AddStreamRequest>
        .FromSuccess(new AddStreamRequest
        {
            ApplicationId = this.applicationId,
            ArchiveId = this.archiveId,
            HasAudio = this.hasAudio,
            HasVideo = this.hasVideo,
            StreamId = this.streamId,
        })
        .Bind(VerifyApplicationId)
        .Bind(VerifyArchiveId)
        .Bind(VerifyStreamId);

    /// <summary>
    ///     Disables the audio on the request.
    /// </summary>
    /// <returns>The builder.</returns>
    public AddStreamRequestBuilder DisableAudio()
    {
        this.hasAudio = false;
        return this;
    }

    /// <summary>
    ///     Disables the video on the request.
    /// </summary>
    /// <returns>The builder.</returns>
    public AddStreamRequestBuilder DisableVideo()
    {
        this.hasVideo = false;
        return this;
    }

    private static Result<AddStreamRequest> VerifyApplicationId(AddStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<AddStreamRequest> VerifyArchiveId(AddStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ArchiveId, nameof(request.ArchiveId));

    private static Result<AddStreamRequest> VerifyStreamId(AddStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.StreamId, nameof(request.StreamId));
}