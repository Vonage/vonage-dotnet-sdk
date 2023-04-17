using System;
using System.Drawing;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Meetings.CreateTheme;

/// <inheritdoc />
internal class CreateThemeRequestBuilder : ICreateThemeRequestBuilder
{
    private readonly Color mainColor;
    private Maybe<string> themeName = Maybe<string>.None;
    private Maybe<Uri> shortCompanyUrl = Maybe<Uri>.None;
    private readonly string brandText;

    internal CreateThemeRequestBuilder(string brandText, Color mainColor)
    {
        this.brandText = brandText;
        this.mainColor = mainColor;
    }

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

    /// <summary>
    ///     Sets the theme name on the builder.
    /// </summary>
    /// <param name="value">The theme name.</param>
    /// <returns>The builder.</returns>
    public ICreateThemeRequestBuilder WithName(string value)
    {
        this.themeName = value;
        return this;
    }

    /// <summary>
    ///     Sets the company Url on the builder.
    /// </summary>
    /// <param name="value">The company Url.</param>
    /// <returns>The builder.</returns>
    public ICreateThemeRequestBuilder WithShortCompanyUrl(Uri value)
    {
        this.shortCompanyUrl = value;
        return this;
    }

    private static Result<CreateThemeRequest> VerifyBrandText(CreateThemeRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.BrandText, nameof(request.BrandText));
}

/// <summary>
///     Represents a builder for CreateThemeRequest.
/// </summary>
public interface ICreateThemeRequestBuilder : IVonageRequestBuilder<CreateThemeRequest>
{
    /// <summary>
    ///     Sets the theme name on the builder.
    /// </summary>
    /// <param name="value">The theme name.</param>
    /// <returns>The builder.</returns>
    ICreateThemeRequestBuilder WithName(string value);

    /// <summary>
    ///     Sets the company Url on the builder.
    /// </summary>
    /// <param name="value">The company Url.</param>
    /// <returns>The builder.</returns>
    ICreateThemeRequestBuilder WithShortCompanyUrl(Uri value);
}