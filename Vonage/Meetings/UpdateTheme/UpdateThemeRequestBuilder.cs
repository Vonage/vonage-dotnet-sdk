using System;
using System.Drawing;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Meetings.UpdateTheme;

/// <inheritdoc />
public class UpdateThemeRequestBuilder : IVonageRequestBuilder<UpdateThemeRequest>
{
    private Maybe<Color> mainColor = Maybe<Color>.None;
    private Maybe<string> themeName = Maybe<string>.None;
    private Maybe<string> brandText = Maybe<string>.None;
    private Maybe<Uri> shortCompanyUrl = Maybe<Uri>.None;
    private readonly string themeId;

    private UpdateThemeRequestBuilder(string themeId) => this.themeId = themeId;

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <param name="themeId">The theme id.</param>
    /// <returns>The builder.</returns>
    public static UpdateThemeRequestBuilder Build(string themeId) => new(themeId);

    /// <inheritdoc />
    public Result<UpdateThemeRequest> Create() => Result<UpdateThemeRequest>
        .FromSuccess(new UpdateThemeRequest
        {
            ThemeId = this.themeId,
            BrandText = this.brandText,
            MainColor = this.mainColor,
            ShortCompanyUrl = this.shortCompanyUrl,
            ThemeName = this.themeName,
        })
        .Bind(VerifyThemeId);

    /// <summary>
    ///     Sets the brand text on the builder.
    /// </summary>
    /// <param name="value">The brand text.</param>
    /// <returns>The builder.</returns>
    public UpdateThemeRequestBuilder WithBrandText(string value)
    {
        this.brandText = value;
        return this;
    }

    /// <summary>
    ///     Sets the main color on the builder.
    /// </summary>
    /// <param name="value">The color.</param>
    /// <returns>The builder.</returns>
    public UpdateThemeRequestBuilder WithColor(Color value)
    {
        this.mainColor = value;
        return this;
    }

    /// <summary>
    ///     Sets the theme name on the builder.
    /// </summary>
    /// <param name="value">The theme name.</param>
    /// <returns>The builder.</returns>
    public UpdateThemeRequestBuilder WithName(string value)
    {
        this.themeName = value;
        return this;
    }

    /// <summary>
    ///     Sets the company Url on the builder.
    /// </summary>
    /// <param name="value">The company Url.</param>
    /// <returns>The builder.</returns>
    public UpdateThemeRequestBuilder WithShortCompanyUrl(Uri value)
    {
        this.shortCompanyUrl = value;
        return this;
    }

    private static Result<UpdateThemeRequest> VerifyThemeId(UpdateThemeRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.ThemeId, nameof(request.ThemeId));
}