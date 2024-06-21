using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Video.ExperienceComposer.GetSessions;

internal struct GetSessionsRequestBuilder : IBuilderForApplicationId, IBuilderForOptional
{
    private Guid applicationId = Guid.Empty;
    private int offset = 0;
    private int count = 50;

    public GetSessionsRequestBuilder()
    {
    }

    public Result<GetSessionsRequest> Create() => Result<GetSessionsRequest>.FromSuccess(
            new GetSessionsRequest
            {
                Count = this.count,
                ApplicationId = this.applicationId,
                Offset = this.offset,
            })
        .Map(InputEvaluation<GetSessionsRequest>.Evaluate)
        .Bind(evaluation =>
            evaluation.WithRules(VerifyApplicationId, VerifyOffset, VerifyCountMinimum, VerifyCountMaximum));

    private static Result<GetSessionsRequest> VerifyApplicationId(GetSessionsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<GetSessionsRequest> VerifyOffset(GetSessionsRequest request) =>
        InputValidation.VerifyHigherOrEqualThan(request, request.Offset, MinimumOffset, nameof(request.Offset));

    private static Result<GetSessionsRequest> VerifyCountMinimum(GetSessionsRequest request) =>
        InputValidation.VerifyHigherOrEqualThan(request, request.Count, MinimumCount, nameof(request.Count));

    private static Result<GetSessionsRequest> VerifyCountMaximum(GetSessionsRequest request) =>
        InputValidation.VerifyLowerOrEqualThan(request, request.Count, MaximumCount, nameof(request.Count));

    private const int MinimumOffset = 0;
    private const int MinimumCount = 50;
    private const int MaximumCount = 1000;

    public IBuilderForOptional WithCount(int value) => this with {count = value};
    public IBuilderForOptional WithOffset(int value) => this with {offset = value};
    public IBuilderForOptional WithApplicationId(Guid value) => this with {applicationId = value};
}

/// <summary>
///     Represents a builder for application Id.
/// </summary>
public interface IBuilderForApplicationId
{
    /// <summary>
    ///     Sets the application Id on the builder.
    /// </summary>
    /// <param name="value">The application Id.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithApplicationId(Guid value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<GetSessionsRequest>
{
    /// <summary>
    ///     Sets the count on the builder.
    /// </summary>
    /// <param name="value">The count.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithCount(int value);

    /// <summary>
    ///     Sets the offset on the builder.
    /// </summary>
    /// <param name="value">The offset.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithOffset(int value);
}