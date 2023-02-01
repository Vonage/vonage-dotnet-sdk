using System;
using System.Drawing;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Meetings.CreateTheme;

/// <inheritdoc />
public class CreateThemeRequestBuilder : IRequestBuilder<CreateThemeRequest>
{
    private readonly Color mainColor;
    private Maybe<string> themeName = Maybe<string>.None;
    private Maybe<Uri> shortCompanyUrl = Maybe<Uri>.None;
    private readonly string brandText;

    private CreateThemeRequestBuilder(string brandText, Color mainColor)
    {
        this.brandText = brandText;
        this.mainColor = mainColor;
    }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <param name="brandText"></param>
    /// <param name="mainColor"></param>
    /// <returns>The builder.</returns>
    public static CreateThemeRequestBuilder Build(string brandText, Color mainColor) => new(brandText, mainColor);

    /// <inheritdoc />
    public Result<CreateThemeRequest> Create() =>
        Result<CreateThemeRequest>
            .FromSuccess(new CreateThemeRequest(
                this.brandText,
                this.mainColor,
                this.themeName,
                this.shortCompanyUrl))
            .Bind(VerifyBrandText);

    /// <summary>
    ///     Sets the theme name on the builder.
    /// </summary>
    /// <param name="value">The theme name.</param>
    /// <returns>The builder.</returns>
    public CreateThemeRequestBuilder WithName(string value)
    {
        this.themeName = value;
        return this;
    }

    /// <summary>
    ///     Sets the company Url on the builder.
    /// </summary>
    /// <param name="value">The company Url.</param>
    /// <returns>The builder.</returns>
    public CreateThemeRequestBuilder WithShortCompanyUrl(Uri value)
    {
        this.shortCompanyUrl = value;
        return this;
    }

    private static Result<CreateThemeRequest> VerifyBrandText(CreateThemeRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.BrandText, nameof(request.BrandText));
}