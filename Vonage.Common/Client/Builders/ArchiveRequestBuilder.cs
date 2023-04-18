using System;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Common.Client.Builders;

/// <summary>
///     Represents a builder for ArchiveRequests, aka requests having an ApplicationId and an ArchiveId.
/// </summary>
/// <typeparam name="T">Type of the underlying request.</typeparam>
public class ArchiveRequestBuilder<T> :
    IVonageRequestBuilder<T>,
    ArchiveRequestBuilder<T>.IBuilderForApplicationId,
    ArchiveRequestBuilder<T>.IBuilderForArchiveId
    where T : IVonageRequest, IHasApplicationId, IHasArchiveId
{
    private readonly Func<Tuple<Guid, Guid>, T> requestInitializer;
    private Guid archiveId;
    private Guid applicationId;

    private ArchiveRequestBuilder(Func<Tuple<Guid, Guid>, T> requestInitializer) =>
        this.requestInitializer = requestInitializer;

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <param name="requestInitializer">The method to initialize a request.</param>
    /// <returns>The builder.</returns>
    public static IBuilderForApplicationId Build(Func<Tuple<Guid, Guid>, T> requestInitializer) =>
        new ArchiveRequestBuilder<T>(requestInitializer);

    /// <inheritdoc />
    public Result<T> Create() => Result<T>
        .FromSuccess(
            this.requestInitializer(new Tuple<Guid, Guid>(this.applicationId, this.archiveId)))
        .Bind(VerifyApplicationId)
        .Bind(VerifyArchiveId);

    /// <inheritdoc />
    public IBuilderForArchiveId WithApplicationId(Guid value)
    {
        this.applicationId = value;
        return this;
    }

    /// <inheritdoc />
    public IVonageRequestBuilder<T> WithArchiveId(Guid value)
    {
        this.archiveId = value;
        return this;
    }

    private static Result<T> VerifyApplicationId(T request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<T> VerifyArchiveId(T request) =>
        InputValidation.VerifyNotEmpty(request, request.ArchiveId, nameof(request.ArchiveId));

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
        IVonageRequestBuilder<T> WithArchiveId(Guid value);
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
}