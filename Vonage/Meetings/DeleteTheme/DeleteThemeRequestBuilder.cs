using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Meetings.DeleteTheme;

/// <summary>
///     Represents a builder for DeleteThemeRequest.
/// </summary>
internal class DeleteThemeRequestBuilder : IBuilderForThemeId, IOptionalBuilder
{
    private bool forceDelete;
    private Guid themeId;

    /// <inheritdoc />
    public Result<DeleteThemeRequest> Create() =>
        Result<DeleteThemeRequest>.FromSuccess(new DeleteThemeRequest
            {
                ThemeId = this.themeId,
                ForceDelete = this.forceDelete,
            })
            .Bind(VerifyThemeId);

    /// <inheritdoc />
    public IOptionalBuilder WithForceDelete()
    {
        this.forceDelete = true;
        return this;
    }

    /// <inheritdoc />
    public IOptionalBuilder WithThemeId(Guid value)
    {
        this.themeId = value;
        return this;
    }

    private static Result<DeleteThemeRequest> VerifyThemeId(DeleteThemeRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ThemeId, nameof(DeleteThemeRequest.ThemeId));
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
///     Represents an optional builder.
/// </summary>
public interface IOptionalBuilder : IVonageRequestBuilder<DeleteThemeRequest>
{
    /// <summary>
    ///     Delete the theme even if theme is used by rooms or as application default theme.
    /// </summary>
    /// <returns>The builder.</returns>
    IOptionalBuilder WithForceDelete();
}