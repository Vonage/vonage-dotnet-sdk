using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.WebUtilities;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Meetings.DeleteTheme;

/// <summary>
///     Represents a request to delete a theme.
/// </summary>
public readonly struct DeleteThemeRequest : IVonageRequest
{
    private DeleteThemeRequest(string themeId, bool forceDelete)
    {
        this.ThemeId = themeId;
        this.ForceDelete = forceDelete;
    }

    /// <summary>
    ///     Delete the theme even if theme is used by rooms or as application default theme.
    /// </summary>
    public bool ForceDelete { get; }

    /// <summary>
    ///     The theme id.
    /// </summary>
    public string ThemeId { get; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Delete, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() =>
        QueryHelpers.AddQueryString($"/beta/meetings/themes/{this.ThemeId}", this.GetQueryStringParameters());

    /// <summary>
    ///     Parses the input into a DeleteThemeRequest.
    /// </summary>
    /// <param name="themeId">The theme id.</param>
    /// <param name="forceDelete">Delete the theme even if theme is used by rooms or as application default theme.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<DeleteThemeRequest> Parse(string themeId, bool forceDelete) =>
        Result<DeleteThemeRequest>
            .FromSuccess(new DeleteThemeRequest(themeId, forceDelete))
            .Bind(VerifyThemeId);

    private Dictionary<string, string> GetQueryStringParameters()
    {
        var parameters = new Dictionary<string, string>();
        if (this.ForceDelete)
        {
            parameters.Add("force", this.ForceDelete.ToString().ToLowerInvariant());
        }

        return parameters;
    }

    private static Result<DeleteThemeRequest> VerifyThemeId(DeleteThemeRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ThemeId, nameof(ThemeId));
}