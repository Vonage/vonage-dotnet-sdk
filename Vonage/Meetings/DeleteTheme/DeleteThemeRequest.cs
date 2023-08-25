using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.WebUtilities;
using Vonage.Common.Client;

namespace Vonage.Meetings.DeleteTheme;

/// <summary>
///     Represents a request to delete a theme.
/// </summary>
public readonly struct DeleteThemeRequest : IVonageRequest
{
    /// <summary>
    ///     Delete the theme even if theme is used by rooms or as application default theme.
    /// </summary>
    public bool ForceDelete { get; internal init; }

    /// <summary>
    ///     The theme id.
    /// </summary>
    public Guid ThemeId { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForThemeId Build() => new DeleteThemeRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Delete, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() =>
        QueryHelpers.AddQueryString($"/v1/meetings/themes/{this.ThemeId}", this.GetQueryStringParameters());

    private Dictionary<string, string> GetQueryStringParameters()
    {
        var parameters = new Dictionary<string, string>();
        if (this.ForceDelete)
        {
            parameters.Add("force", this.ForceDelete.ToString().ToLowerInvariant());
        }

        return parameters;
    }
}