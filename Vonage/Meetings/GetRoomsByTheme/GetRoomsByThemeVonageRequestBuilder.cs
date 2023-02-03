using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Meetings.GetRoomsByTheme;

/// <inheritdoc />
public class GetRoomsByThemeVonageRequestBuilder : IVonageRequestBuilder<GetRoomsByThemeRequest>
{
    private Maybe<string> startId = Maybe<string>.None;
    private Maybe<string> endId = Maybe<string>.None;
    private readonly string themeId;

    private GetRoomsByThemeVonageRequestBuilder(string themeId) => this.themeId = themeId;

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <param name="brandText"></param>
    /// <returns>The builder.</returns>
    public static GetRoomsByThemeVonageRequestBuilder Build(string brandText) => new(brandText);

    /// <inheritdoc />
    public Result<GetRoomsByThemeRequest> Create() =>
        Result<GetRoomsByThemeRequest>
            .FromSuccess(new GetRoomsByThemeRequest
            {
                ThemeId = this.themeId,
                EndId = this.endId,
                StartId = this.startId,
            })
            .Bind(VerifyThemeId);

    /// <summary>
    ///     Sets the end id on the builder.
    /// </summary>
    /// <param name="value">The end id.</param>
    /// <returns>The builder.</returns>
    public GetRoomsByThemeVonageRequestBuilder WithEndId(string value)
    {
        this.endId = value;
        return this;
    }

    /// <summary>
    ///     Sets the start id on the builder.
    /// </summary>
    /// <param name="value">The start id.</param>
    /// <returns>The builder.</returns>
    public GetRoomsByThemeVonageRequestBuilder WithStartId(string value)
    {
        this.startId = value;
        return this;
    }

    private static Result<GetRoomsByThemeRequest> VerifyThemeId(GetRoomsByThemeRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.ThemeId, nameof(request.ThemeId));
}