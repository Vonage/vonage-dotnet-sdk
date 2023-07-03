using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Meetings.GetRooms;

internal class GetRoomsRequestBuilder : IOptionalBuilder
{
    private const int MinPageSize = 1;
    private Maybe<int> pageSize = Maybe<int>.None;
    private Maybe<int> endId = Maybe<int>.None;
    private Maybe<int> startId = Maybe<int>.None;

    /// <inheritdoc />
    public Result<GetRoomsRequest> Create() => Result<GetRoomsRequest>.FromSuccess(new GetRoomsRequest
        {
            EndId = this.endId,
            StartId = this.startId,
            PageSize = this.pageSize,
        })
        .Map(InputEvaluation<GetRoomsRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules(VerifyPageSize));

    /// <inheritdoc />
    public IOptionalBuilder WithEndId(int value)
    {
        this.endId = value;
        return this;
    }

    /// <inheritdoc />
    public IOptionalBuilder WithPageSize(int value)
    {
        this.pageSize = value;
        return this;
    }

    /// <inheritdoc />
    public IOptionalBuilder WithStartId(int value)
    {
        this.startId = value;
        return this;
    }

    private static Result<GetRoomsRequest> VerifyPageSize(GetRoomsRequest request) =>
        request.PageSize.Match(
            value => InputValidation.VerifyHigherOrEqualThan(request, value, MinPageSize,
                nameof(GetRoomsRequest.PageSize)),
            () => request);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IOptionalBuilder : IVonageRequestBuilder<GetRoomsRequest>
{
    /// <summary>
    ///     Sets the end id on the builder.
    /// </summary>
    /// <param name="value">The ID to end returning events at (excluding end_id itself).</param>
    /// <returns>The builder.</returns>
    public IOptionalBuilder WithEndId(int value);

    /// <summary>
    ///     Sets the page size on the builder.
    /// </summary>
    /// <param name="value">The maximum number of rooms in the current page.</param>
    /// <returns>The builder.</returns>
    public IOptionalBuilder WithPageSize(int value);

    /// <summary>
    ///     Sets the start id on the builder.
    /// </summary>
    /// <param name="value"> The ID to start returning events at.</param>
    /// <returns>The builder.</returns>
    public IOptionalBuilder WithStartId(int value);
}