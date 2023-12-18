using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Video.Broadcast.GetBroadcasts;

internal class GetBroadcastsRequestBuilder : IBuilderForApplicationId, IBuilderForOptional
{
    private const int MaxCount = 1000;
    private Guid applicationId;
    private int count = 50;
    private int offset;
    private Maybe<string> sessionId = Maybe<string>.None;

    /// <inheritdoc />
    public Result<GetBroadcastsRequest> Create() => Result<GetBroadcastsRequest>.FromSuccess(new GetBroadcastsRequest
        {
            Count = this.count,
            Offset = this.offset,
            ApplicationId = this.applicationId,
            SessionId = this.sessionId,
        })
        .Map(InputEvaluation<GetBroadcastsRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules(VerifyApplicationId, VerifyCount, VerifyOffset));

    /// <inheritdoc />
    public IBuilderForOptional WithApplicationId(Guid value)
    {
        this.applicationId = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithCount(int value)
    {
        this.count = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithOffset(int value)
    {
        this.offset = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithSessionId(string value)
    {
        this.sessionId = value;
        return this;
    }

    private static Result<GetBroadcastsRequest> VerifyApplicationId(GetBroadcastsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<GetBroadcastsRequest> VerifyCount(GetBroadcastsRequest request) =>
        InputValidation.VerifyNotNegative(request, request.Count, nameof(request.Count))
            .Bind(_ => InputValidation.VerifyLowerOrEqualThan(request, request.Count, MaxCount, nameof(request.Count)));

    private static Result<GetBroadcastsRequest> VerifyOffset(GetBroadcastsRequest request) =>
        InputValidation.VerifyNotNegative(request, request.Offset, nameof(request.Offset));
}

/// <summary>
///     Represents a GetBroadcastRequestBuilder that allows to set the ApplicationId.
/// </summary>
public interface IBuilderForApplicationId
{
    /// <summary>
    ///     Sets the ApplicationId on the builder.
    /// </summary>
    /// <param name="value">The application id.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithApplicationId(Guid value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<GetBroadcastsRequest>
{
    /// <summary>
    ///     Sets a count query parameter to limit the number of archives to be returned. The default number of archives
    ///     returned
    ///     is 50 (or fewer, if there are fewer than 50 archives). The maximum number of archives the call will return is 1000.
    /// </summary>
    /// <param name="value">The count.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithCount(int value);

    /// <summary>
    ///     Sets an offset query parameters to specify the index offset of the first archive. 0 is offset of the most recently
    ///     started archive (excluding deleted archive). 1 is the offset of the archive that started prior to the most recent
    ///     archive. The default value is 0.
    /// </summary>
    /// <param name="value">The offset.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithOffset(int value);

    /// <summary>
    ///     Sets a sessionId query parameter to list archives for a specific session ID. (This is useful when listing multiple
    ///     archives for an automatically archived session.)
    /// </summary>
    /// <param name="value"></param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithSessionId(string value);
}