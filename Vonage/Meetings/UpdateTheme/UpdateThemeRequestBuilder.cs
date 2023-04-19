using System;
using System.Drawing;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Meetings.UpdateTheme;

internal class UpdateThemeRequestBuilder : IBuilderForThemeId, IBuilderForOptional
{
    private Guid themeId;
    private Maybe<Color> mainColor = Maybe<Color>.None;
    private Maybe<string> themeName = Maybe<string>.None;
    private Maybe<string> brandText = Maybe<string>.None;
    private Maybe<Uri> shortCompanyUrl = Maybe<Uri>.None;

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
    public IBuilderForOptional WithBrandText(string value)
    {
        this.brandText = value;
        return this;
    }

    /// <summary>
    ///     Sets the main color on the builder.
    /// </summary>
    /// <param name="value">The color.</param>
    /// <returns>The builder.</returns>
    public IBuilderForOptional WithColor(Color value)
    {
        this.mainColor = value;
        return this;
    }

    /// <summary>
    ///     Sets the theme name on the builder.
    /// </summary>
    /// <param name="value">The theme name.</param>
    /// <returns>The builder.</returns>
    public IBuilderForOptional WithName(string value)
    {
        this.themeName = value;
        return this;
    }

    /// <summary>
    ///     Sets the company Url on the builder.
    /// </summary>
    /// <param name="value">The company Url.</param>
    /// <returns>The builder.</returns>
    public IBuilderForOptional WithShortCompanyUrl(Uri value)
    {
        this.shortCompanyUrl = value;
        return this;
    }

    public IBuilderForOptional WithThemeId(Guid value)
    {
        this.themeId = value;
        return this;
    }

    private static Result<UpdateThemeRequest> VerifyThemeId(UpdateThemeRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.ThemeId, nameof(request.ThemeId));
}

/// <summary>
///     Represents a builder for ThemeId.
/// </summary>
public interface IBuilderForThemeId
{
    /// <summary>
    ///     Sets the theme Id.
    /// </summary>
    /// <param name="value">The theme Id.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithThemeId(Guid value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<UpdateThemeRequest>
{
    /// <summary>
    ///     Sets the brand text on the builder.
    /// </summary>
    /// <param name="value">The brand text.</param>
    /// <returns>The builder.</returns>
    public IBuilderForOptional WithBrandText(string value);

    /// <summary>
    ///     Sets the main color on the builder.
    /// </summary>
    /// <param name="value">The color.</param>
    /// <returns>The builder.</returns>
    public IBuilderForOptional WithColor(Color value);

    /// <summary>
    ///     Sets the theme name on the builder.
    /// </summary>
    /// <param name="value">The theme name.</param>
    /// <returns>The builder.</returns>
    public IBuilderForOptional WithName(string value);

    /// <summary>
    ///     Sets the company Url on the builder.
    /// </summary>
    /// <param name="value">The company Url.</param>
    /// <returns>The builder.</returns>
    public IBuilderForOptional WithShortCompanyUrl(Uri value);
}