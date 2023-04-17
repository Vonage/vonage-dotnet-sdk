using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Server.Video.Archives.ChangeLayout;

/// <summary>
///     Represents a builder for ChangeLayoutRequest.
/// </summary>
internal class ChangeLayoutRequestBuilder : IBuilderForArchiveId, IBuilderForApplicationId, IBuilderForLayout,
    IVonageRequestBuilder<ChangeLayoutRequest>
{
    private Guid applicationId;
    private Guid archiveId;
    private Layout layout;

    /// <inheritdoc />
    public Result<ChangeLayoutRequest> Create() => Result<ChangeLayoutRequest>
        .FromSuccess(new ChangeLayoutRequest
        {
            ApplicationId = this.applicationId,
            ArchiveId = this.archiveId,
            Layout = this.layout,
        })
        .Bind(VerifyApplicationId)
        .Bind(VerifyArchiveId);

    /// <inheritdoc />
    public IBuilderForArchiveId WithApplicationId(Guid value)
    {
        this.applicationId = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForLayout WithArchiveId(Guid value)
    {
        this.archiveId = value;
        return this;
    }

    /// <inheritdoc />
    public IVonageRequestBuilder<ChangeLayoutRequest> WithLayout(Layout value)
    {
        this.layout = value;
        return this;
    }

    private static Result<ChangeLayoutRequest> VerifyApplicationId(ChangeLayoutRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<ChangeLayoutRequest> VerifyArchiveId(ChangeLayoutRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ArchiveId, nameof(request.ArchiveId));
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
    IBuilderForLayout WithArchiveId(Guid value);
}

/// <summary>
///     Represents a builder for Layout.
/// </summary>
public interface IBuilderForLayout
{
    /// <summary>
    ///     Sets the Layout.
    /// </summary>
    /// <param name="value">The Layout.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<ChangeLayoutRequest> WithLayout(Layout value);
}