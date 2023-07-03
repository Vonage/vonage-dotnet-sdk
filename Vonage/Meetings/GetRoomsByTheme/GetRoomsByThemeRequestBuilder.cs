using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Meetings.GetRoomsByTheme;

internal class GetRoomsByThemeRequestBuilder : IBuilderForThemeId, IOptionalBuilder
{
    private const int MinPageSize = 1;
    private Guid themeId;
    private Maybe<int> startId = Maybe<int>.None;
    private Maybe<int> endId = Maybe<int>.None;
    private Maybe<int> pageSize = Maybe<int>.None;

    /// <inheritdoc />
    public Result<GetRoomsByThemeRequest> Create() =>
        Result<GetRoomsByThemeRequest>
            .FromSuccess(new GetRoomsByThemeRequest
            {
                ThemeId = this.themeId,
                EndId = this.endId,
                StartId = this.startId,
                PageSize = this.pageSize,
            })
            .Map(InputEvaluation<GetRoomsByThemeRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyThemeId, VerifyPageSize));

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

    /// <inheritdoc />
    public IOptionalBuilder WithThemeId(Guid value)
    {
        this.themeId = value;
        return this;
    }

    private static Result<GetRoomsByThemeRequest> VerifyPageSize(GetRoomsByThemeRequest request) =>
        request.PageSize.Match(
            value => InputValidation.VerifyHigherOrEqualThan(request, value, MinPageSize,
                nameof(GetRoomsByThemeRequest.PageSize)),
            () => request);

    private static Result<GetRoomsByThemeRequest> VerifyThemeId(GetRoomsByThemeRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.ThemeId, nameof(request.ThemeId));
}

/// <summary>
///     Represents a builder for ThemeId.
/// </summary>
public interface IBuilderForThemeId
{
    /// <summary>
    ///     Sets the ThemeId.
    /// </summary>
    /// <param name="value">The theme Id.</param>
    /// <returns>The builder.</returns>
    IOptionalBuilder WithThemeId(Guid value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IOptionalBuilder : IVonageRequestBuilder<GetRoomsByThemeRequest>
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