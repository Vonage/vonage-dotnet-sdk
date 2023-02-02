using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Meetings.Common;

namespace Vonage.Meetings.UpdateThemeLogo;

/// <summary>
///     Represents a request to update a theme logo.
/// </summary>
public readonly struct UpdateThemeLogoRequest
{
    private UpdateThemeLogoRequest(string filePath, string themeId, ThemeLogoType type)
    {
        this.FilePath = filePath;
        this.ThemeId = themeId;
        this.Type = type;
    }

    /// <summary>
    ///     Absolute path to the logo image.
    /// </summary>
    public string FilePath { get; }

    /// <summary>
    ///     Id of the theme which the logo will be associated with.
    /// </summary>
    public string ThemeId { get; }

    /// <summary>
    ///     The logo type to upload.
    /// </summary>
    public ThemeLogoType Type { get; }

    /// <summary>
    ///     Parses the input into a UpdateThemeLogoRequest.
    /// </summary>
    /// <param name="themeId">Id of the theme which the logo will be associated with.</param>
    /// <param name="logoType">The logo type to upload.</param>
    /// <param name="filePath">Absolute path to the logo image.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<UpdateThemeLogoRequest> Parse(string themeId, ThemeLogoType logoType, string filePath) =>
        Result<UpdateThemeLogoRequest>
            .FromSuccess(new UpdateThemeLogoRequest(filePath, themeId, logoType))
            .Bind(VerifyThemeId)
            .Bind(VerifyFilepath);

    private static Result<UpdateThemeLogoRequest> VerifyFilepath(UpdateThemeLogoRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.FilePath, nameof(FilePath));

    private static Result<UpdateThemeLogoRequest> VerifyThemeId(UpdateThemeLogoRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ThemeId, nameof(ThemeId));
}