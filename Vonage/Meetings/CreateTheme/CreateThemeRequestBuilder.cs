using System;
using System.Drawing;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Meetings.CreateTheme;

/// <summary>
///     Represents a builder for CreateThemeRequest.
/// </summary>
internal class CreateThemeRequestBuilder : IBuilderForBrand, IBuilderForColor, IBuilderForOptional
{
    private Color mainColor;
    private Maybe<string> themeName = Maybe<string>.None;
    private Maybe<Uri> shortCompanyUrl = Maybe<Uri>.None;
    private string brandText;

    /// <inheritdoc />
    public Result<CreateThemeRequest> Create() =>
        Result<CreateThemeRequest>
            .FromSuccess(new CreateThemeRequest
            {
                BrandText = this.brandText,
                MainColor = this.mainColor,
                ThemeName = this.themeName,
                ShortCompanyUrl = this.shortCompanyUrl,
            })
            .Bind(VerifyBrandText);

    /// <inheritdoc />
    public IBuilderForColor WithBrand(string value)
    {
        this.brandText = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithColor(Color value)
    {
        this.mainColor = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithName(string value)
    {
        this.themeName = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithShortCompanyUrl(Uri value)
    {
        this.shortCompanyUrl = value;
        return this;
    }

    private static Result<CreateThemeRequest> VerifyBrandText(CreateThemeRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.BrandText, nameof(request.BrandText));
}

/// <summary>
/// Represents a builder for Brand.
/// </summary>
public interface IBuilderForBrand
{
    /// <summary>
    ///     Sets the brand.
    /// </summary>
    /// <param name="value">The brand.</param>
    /// <returns>The builder.</returns>
    IBuilderForColor WithBrand(string value);
}

/// <summary>
///     Represents a builder for Color.
/// </summary>
public interface IBuilderForColor
{
    /// <summary>
    ///     Sets the brand.
    /// </summary>
    /// <param name="value">The brand.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithColor(Color value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<CreateThemeRequest>
{
    /// <summary>
    ///     Sets the theme name on the builder.
    /// </summary>
    /// <param name="value">The theme name.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithName(string value);

    /// <summary>
    ///     Sets the company Url on the builder.
    /// </summary>
    /// <param name="value">The company Url.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithShortCompanyUrl(Uri value);
}