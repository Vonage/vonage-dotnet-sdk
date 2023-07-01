using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Server.Video.Archives.GetArchive;

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
        .Map(InputEvaluation<GetArchiveRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules(VerifyApplicationId, VerifyArchiveId));

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

    private static Result<GetArchiveRequest> VerifyApplicationId(GetArchiveRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<GetArchiveRequest> VerifyArchiveId(GetArchiveRequest request) =>
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
    IVonageRequestBuilder<GetArchiveRequest> WithArchiveId(Guid value);
}