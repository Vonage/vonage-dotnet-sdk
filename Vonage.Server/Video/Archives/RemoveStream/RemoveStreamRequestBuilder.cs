using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Server.Video.Archives.RemoveStream;

/// <summary>
///     Represents a builder for RemoveStreamRequest.
/// </summary>
internal class RemoveStreamRequestBuilder : IBuilderForArchiveId, IBuilderForApplicationId, IBuilderForStreamId,
    IVonageRequestBuilder<RemoveStreamRequest>
{
    private Guid applicationId;
    private Guid archiveId;
    private Guid streamId;

    /// <inheritdoc />
    public Result<RemoveStreamRequest> Create() => Result<RemoveStreamRequest>
        .FromSuccess(new RemoveStreamRequest
        {
            ApplicationId = this.applicationId,
            ArchiveId = this.archiveId,
            StreamId = this.streamId,
        })
        .Bind(VerifyApplicationId)
        .Bind(VerifyArchiveId)
        .Bind(VerifyStreamId);

    /// <inheritdoc />
    public IBuilderForArchiveId WithApplicationId(Guid value)
    {
        this.applicationId = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForStreamId WithArchiveId(Guid value)
    {
        this.archiveId = value;
        return this;
    }

    /// <inheritdoc />
    public IVonageRequestBuilder<RemoveStreamRequest> WithStreamId(Guid value)
    {
        this.streamId = value;
        return this;
    }

    private static Result<RemoveStreamRequest> VerifyApplicationId(RemoveStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<RemoveStreamRequest> VerifyArchiveId(RemoveStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ArchiveId, nameof(request.ArchiveId));

    private static Result<RemoveStreamRequest> VerifyStreamId(RemoveStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.StreamId, nameof(request.StreamId));
}

/// <summary>
///     Represents a builder for ApplicationId.
/// </summary>
public interface IBuilderForApplicationId
{
    /// <summary>
    ///     Sets the ApplicationId.
    /// </summary>
    /// <param name="value">The ApplicationId.</param>
    /// <returns>The builder.</returns>
    IBuilderForArchiveId WithApplicationId(Guid value);
}

/// <summary>
///     Represents a builder for ArchiveId.
/// </summary>
public interface IBuilderForArchiveId
{
    /// <summary>
    ///     Sets the ArchiveId.
    /// </summary>
    /// <param name="value">The ArchiveId.</param>
    /// <returns>The builder.</returns>
    IBuilderForStreamId WithArchiveId(Guid value);
}

/// <summary>
///     Represents a builder for StreamId.
/// </summary>
public interface IBuilderForStreamId
{
    /// <summary>
    ///     Sets the StreamId.
    /// </summary>
    /// <param name="value">The StreamId.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<RemoveStreamRequest> WithStreamId(Guid value);
}