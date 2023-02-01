using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Meetings.GetRoomsByTheme;

/// <inheritdoc />
public class GetRoomsByThemeRequestBuilder : IRequestBuilder<GetRoomsByThemeRequest>
{
    private Maybe<string> startId = Maybe<string>.None;
    private Maybe<string> endId = Maybe<string>.None;
    private readonly string themeId;

    private GetRoomsByThemeRequestBuilder(string themeId) => this.themeId = themeId;

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <param name="brandText"></param>
    /// <returns>The builder.</returns>
    public static GetRoomsByThemeRequestBuilder Build(string brandText) => new(brandText);

    /// <inheritdoc />
    public Result<GetRoomsByThemeRequest> Create() =>
        Result<GetRoomsByThemeRequest>
            .FromSuccess(new GetRoomsByThemeRequest(
                this.themeId,
                this.startId,
                this.endId))
            .Bind(VerifyBrandText);

    /// <summary>
    ///     Sets the end id on the builder.
    /// </summary>
    /// <param name="value">The end id.</param>
    /// <returns>The builder.</returns>
    public GetRoomsByThemeRequestBuilder WithEndId(string value)
    {
        this.endId = value;
        return this;
    }

    /// <summary>
    ///     Sets the start id on the builder.
    /// </summary>
    /// <param name="value">The start id.</param>
    /// <returns>The builder.</returns>
    public GetRoomsByThemeRequestBuilder WithStartId(string value)
    {
        this.startId = value;
        return this;
    }

    private static Result<GetRoomsByThemeRequest> VerifyBrandText(GetRoomsByThemeRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.ThemeId, nameof(request.ThemeId));
}