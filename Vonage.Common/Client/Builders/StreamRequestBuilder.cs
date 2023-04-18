using System;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Common.Client.Builders;

/// <summary>
///     Represents a builder for StreamRequests, aka requests having an ApplicationId, an ArchiveId and a StreamId.
/// </summary>
/// <typeparam name="T">Type of the underlying request.</typeparam>
public class StreamRequestBuilder<T> :
    IVonageRequestBuilder<T>,
    IBuilderForApplicationId<T>,
    IBuilderForArchiveId<T>,
    IBuilderForStreamId<T>
    where T : IVonageRequest, IHasApplicationId, IHasArchiveId, IHasStreamId
{
    private readonly Func<Guid, Guid, Guid, T> requestInitializer;
    private Guid archiveId;
    private Guid applicationId;
    private Guid streamId;

    private StreamRequestBuilder(Func<Guid, Guid, Guid, T> requestInitializer) =>
        this.requestInitializer = requestInitializer;

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <param name="requestInitializer">The method to initialize a request.</param>
    /// <returns>The builder.</returns>
    public static IBuilderForApplicationId<T> Build(Func<Guid, Guid, Guid, T> requestInitializer) =>
        new StreamRequestBuilder<T>(requestInitializer);

    /// <inheritdoc />
    public Result<T> Create() => Result<T>
        .FromSuccess(this.requestInitializer(this.applicationId, this.archiveId, this.streamId))
        .Bind(VerifyApplicationId)
        .Bind(VerifyArchiveId)
        .Bind(VerifyStreamId);

    /// <inheritdoc />
    public IBuilderForArchiveId<T> WithApplicationId(Guid value)
    {
        this.applicationId = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForStreamId<T> WithArchiveId(Guid value)
    {
        this.archiveId = value;
        return this;
    }

    /// <inheritdoc />
    public IVonageRequestBuilder<T> WithStreamId(Guid value)
    {
        this.streamId = value;
        return this;
    }

    private static Result<T> VerifyApplicationId(T request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<T> VerifyArchiveId(T request) =>
        InputValidation.VerifyNotEmpty(request, request.ArchiveId, nameof(request.ArchiveId));

    private static Result<T> VerifyStreamId(T request) =>
        InputValidation.VerifyNotEmpty(request, request.StreamId, nameof(request.StreamId));
}