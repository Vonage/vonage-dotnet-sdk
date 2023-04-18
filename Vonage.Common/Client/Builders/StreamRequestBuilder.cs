using System;
using Vonage.Common.Monads;

namespace Vonage.Common.Client.Builders;

/// <summary>
///     Represents a builder for StreamRequests, aka requests having an ApplicationId, an ArchiveId and a StreamId.
/// </summary>
/// <typeparam name="T">Type of the underlying request.</typeparam>
public class StreamRequestBuilder<T> :
    IVonageRequestBuilder<T>,
    StreamRequestBuilder<T>.IBuilderForApplicationId,
    StreamRequestBuilder<T>.IBuilderForArchiveId,
    StreamRequestBuilder<T>.IBuilderForStreamId
    where T : IVonageRequest, IHasApplicationId, IHasArchiveId, IHasStreamId
{
    private readonly Func<Tuple<Guid, Guid, Guid>, T> requestInitializer;
    private Guid archiveId;
    private Guid applicationId;
    private Guid streamId;

    private StreamRequestBuilder(Func<Tuple<Guid, Guid, Guid>, T> requestInitializer) =>
        this.requestInitializer = requestInitializer;

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <param name="requestInitializer">The method to initialize a request.</param>
    /// <returns>The builder.</returns>
    public static IBuilderForApplicationId Build(Func<Tuple<Guid, Guid, Guid>, T> requestInitializer) =>
        new StreamRequestBuilder<T>(requestInitializer);

    /// <inheritdoc />
    public Result<T> Create() => Result<T>
        .FromSuccess(
            this.requestInitializer(new Tuple<Guid, Guid, Guid>(this.applicationId, this.archiveId, this.streamId)))
        .Bind(BuilderExtensions.VerifyApplicationId)
        .Bind(BuilderExtensions.VerifyArchiveId)
        .Bind(BuilderExtensions.VerifyStreamId);

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
    public IVonageRequestBuilder<T> WithStreamId(Guid value)
    {
        this.streamId = value;
        return this;
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
    ///     Represents a builder for StreamId.
    /// </summary>
    public interface IBuilderForStreamId
    {
        /// <summary>
        ///     Sets the StreamId.
        /// </summary>
        /// <param name="value">The StreamId.</param>
        /// <returns>The builder.</returns>
        IVonageRequestBuilder<T> WithStreamId(Guid value);
    }
}