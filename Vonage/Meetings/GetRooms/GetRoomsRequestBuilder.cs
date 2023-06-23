using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Meetings.GetRooms;

internal class GetRoomsRequestBuilder : IOptionalBuilder
{
    private const int MinPageSize = 1;
    private Maybe<int> pageSize = Maybe<int>.None;
    private Maybe<string> endId = Maybe<string>.None;
    private Maybe<string> startId = Maybe<string>.None;

    public Result<GetRoomsRequest> Create() => Result<GetRoomsRequest>.FromSuccess(new GetRoomsRequest
        {
            EndId = this.endId,
            StartId = this.startId,
            PageSize = this.pageSize,
        })
        .Bind(VerifyStartId)
        .Bind(VerifyEndId)
        .Bind(VerifyPageSize);

    public IOptionalBuilder WithEndId(string value)
    {
        this.endId = value;
        return this;
    }

    public IOptionalBuilder WithPageSize(int value)
    {
        this.pageSize = value;
        return this;
    }

    public IOptionalBuilder WithStartId(string value)
    {
        this.startId = value;
        return this;
    }

    private static Result<GetRoomsRequest> VerifyEndId(GetRoomsRequest request) =>
        request.EndId.Match(
            value => InputValidation.VerifyNotEmpty(request, value, nameof(GetRoomsRequest.EndId)),
            () => request);

    private static Result<GetRoomsRequest> VerifyPageSize(GetRoomsRequest request) =>
        request.PageSize.Match(
            value => InputValidation.VerifyHigherOrEqualThan(request, value, MinPageSize,
                nameof(GetRoomsRequest.PageSize)),
            () => request);

    private static Result<GetRoomsRequest> VerifyStartId(GetRoomsRequest request) =>
        request.StartId.Match(
            value => InputValidation.VerifyNotEmpty(request, value, nameof(GetRoomsRequest.StartId)),
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
    public IOptionalBuilder WithEndId(string value);

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
    public IOptionalBuilder WithStartId(string value);
}