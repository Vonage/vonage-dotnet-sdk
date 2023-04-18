using System;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Archives.GetArchive;

/// <summary>
///     Represents a builder for GetArchiveRequest.
/// </summary>
internal class GetArchiveRequestBuilder : IBuilderForApplicationId, IBuilderForArchiveId,
    IVonageRequestBuilder<GetArchiveRequest>
{
    private Guid applicationId;
    private Guid archiveId;

    /// <inheritdoc />
    public Result<GetArchiveRequest> Create() => Result<GetArchiveRequest>
        .FromSuccess(new GetArchiveRequest
        {
            ApplicationId = this.applicationId,
            ArchiveId = this.archiveId,
        })
        .Bind(BuilderExtensions.VerifyApplicationId)
        .Bind(BuilderExtensions.VerifyArchiveId);

    /// <inheritdoc />
    public IBuilderForArchiveId WithApplicationId(Guid value)
    {
        this.applicationId = value;
        return this;
    }

    /// <inheritdoc />
    public IVonageRequestBuilder<GetArchiveRequest> WithArchiveId(Guid value)
    {
        this.archiveId = value;
        return this;
    }
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
    IVonageRequestBuilder<GetArchiveRequest> WithArchiveId(Guid value);
}