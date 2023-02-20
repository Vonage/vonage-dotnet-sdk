using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Server.Video.Broadcast.GetBroadcasts;

/// <inheritdoc />
public class GetBroadcastsRequestBuilder : IVonageRequestBuilder<GetBroadcastsRequest>
{
    private const int MaxCount = 1000;
    private readonly Guid applicationId;
    private int count = 50;
    private int offset;
    private Maybe<string> sessionId = Maybe<string>.None;

    private GetBroadcastsRequestBuilder(Guid applicationId) => this.applicationId = applicationId;

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <param name="applicationId">The Vonage Application UUID.</param>
    /// <returns>The builder.</returns>
    public static GetBroadcastsRequestBuilder Build(Guid applicationId) =>
        new(applicationId);

    /// <inheritdoc />
    public Result<GetBroadcastsRequest> Create() => Result<GetBroadcastsRequest>.FromSuccess(new GetBroadcastsRequest
        {
            Count = this.count,
            Offset = this.offset,
            ApplicationId = this.applicationId,
            SessionId = this.sessionId,
        })
        .Bind(VerifyApplicationId)
        .Bind(VerifyCount)
        .Bind(VerifyOffset);

    /// <summary>
    ///     Sets a count query parameter to limit the number of archives to be returned. The default number of archives
    ///     returned
    ///     is 50 (or fewer, if there are fewer than 50 archives). The maximum number of archives the call will return is 1000.
    /// </summary>
    /// <param name="value">The count.</param>
    /// <returns>The builder.</returns>
    public GetBroadcastsRequestBuilder WithCount(int value)
    {
        this.count = value;
        return this;
    }

    /// <summary>
    ///     Sets an offset query parameters to specify the index offset of the first archive. 0 is offset of the most recently
    ///     started archive (excluding deleted archive). 1 is the offset of the archive that started prior to the most recent
    ///     archive. The default value is 0.
    /// </summary>
    /// <param name="value">The offset.</param>
    /// <returns>The builder.</returns>
    public GetBroadcastsRequestBuilder WithOffset(int value)
    {
        this.offset = value;
        return this;
    }

    /// <summary>
    ///     Sets a sessionId query parameter to list archives for a specific session ID. (This is useful when listing multiple
    ///     archives for an automatically archived session.)
    /// </summary>
    /// <param name="value"></param>
    /// <returns>The builder.</returns>
    public GetBroadcastsRequestBuilder WithSessionId(string value)
    {
        this.sessionId = value;
        return this;
    }

    private static Result<GetBroadcastsRequest> VerifyApplicationId(GetBroadcastsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<GetBroadcastsRequest> VerifyCount(GetBroadcastsRequest request) =>
        InputValidation.VerifyNotNegative(request, request.Count, nameof(request.Count))
            .Bind(_ => InputValidation.VerifyLowerOrEqualThan(request, request.Count, MaxCount, nameof(request.Count)));

    private static Result<GetBroadcastsRequest> VerifyOffset(GetBroadcastsRequest request) =>
        InputValidation.VerifyNotNegative(request, request.Offset, nameof(request.Offset));
}